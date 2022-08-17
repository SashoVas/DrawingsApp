using DrawingsApp.Controllers;
using DrawingsApp.Data.Common;
using DrawingsApp.Groups.Models.InputModels.Group;
using DrawingsApp.Groups.Services.Contracts;
using DrawingsApp.Messages.User;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    public class GroupController : ApiController
    {
        private readonly IGroupService groupService;
        private readonly IUserService userService;
        private readonly IBus publisher;
        public GroupController(IGroupService groupService, IUserService userService, IBus publisher)
        {
            this.groupService = groupService;
            this.userService = userService;
            this.publisher = publisher;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var group = await groupService.GetGroup(id,GetUserId());
            if (group is null)
            {
                return NotFound();
            }
            return Ok(group);
        }
        [HttpGet]
        public async Task<ActionResult> Search([FromQuery]SearchGroupInputModel input)
            =>Ok(await groupService.Search(
                input.Name??"",
                input.Tags,
                input.UserId=="mine"?GetUserId():input.UserId,
                input.GroupType,
                input.Order,
                GetUserId()));
        [HttpGet("User")]
        public async Task<ActionResult> GetGroupsByUser(bool isLess=false) 
            => Ok(await groupService.GetGropusByUser(GetUserId(),isLess));
        [HttpGet("Top")]
        public async Task<ActionResult> GetTopGroups(bool isLess = false)
            => Ok(await groupService.GetTopGroups(GetUserId(),isLess));

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
            await publisher.Publish(new PromoteUserRoleInGroupMessage
            {
                UserId = GetUserId(),
                GroupId = groupId,
                Role = Role.Admin
            });
            return CreatedAtAction(nameof(Get), new { id = groupId },groupId);
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
