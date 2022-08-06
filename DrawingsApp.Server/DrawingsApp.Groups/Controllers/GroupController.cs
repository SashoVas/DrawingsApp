using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.Group;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    public class GroupController : ApiController
    {
        private readonly IGroupService groupService;
        private readonly IUserService userService;
        public GroupController(IGroupService groupService, IUserService userService)
        {
            this.groupService = groupService;
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var group = await groupService.GetGroup(id);
            if (group is null)
            {
                return NotFound();
            }
            return Ok(group);
        }
        [HttpGet]
        public async Task<ActionResult> GetByName(string name) 
            => Ok(await groupService.GetGropus(name));
        [HttpGet("User")]
        public async Task<ActionResult> GetGroupsByUser() 
            => Ok(await groupService.GetGropusByUser(GetUserId()));
        [HttpGet("Top")]
        public async Task<ActionResult> GetTopGroups()
            => Ok(await groupService.GetTopGroups());

        [HttpPost]
        public async Task<ActionResult> Create(CreateGroupInputModel input)
        {
            var userId = GetUserId();
            if (!await userService.UserExists(userId))
            {
                await userService.CreateUser(userId,User.Identity.Name);
            }
            var groupId = await groupService.CreateGroup(input.Title, input.MoreInfo,input.ImgUrl, input.GroupType, input.Tags);
            await userService.JoinGroup(userId, groupId);
            await userService.PromoteUser(userId, groupId);
            return Created(nameof(Get), groupId);
        }
        [HttpPut]
        public async Task<ActionResult> Update(UpdateGroupInputModel input)
        {
            if (!await userService.IsAdmin(GetUserId(),input.GroupId))
            {
                return Unauthorized();
            }
            await groupService.UpdateGroup(input.GroupId,input.Title,input.MoreInfo,input.ImgUrl,input.GroupType,input.Tags);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await userService.IsAdmin(GetUserId(), id))
            {
                return Unauthorized();
            }
            await groupService.DeleteGroup(id);
            return Ok();
        }
    }
}
