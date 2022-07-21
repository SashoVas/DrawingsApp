using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.Group;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrawingsApp.Groups.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Get(int id) =>
            Ok(await groupService.GetGroup(id));
        [HttpPost]
        public async Task<ActionResult> Create(CreateGroupInputModel input)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await userService.UserExists(userId))
            {
                await userService.CreateUser(userId,User.Identity.Name);
            }
            var groupId = await groupService.CreateGroup(input.Title, input.MoreInfo, input.GroupType, input.Tags);
            await userService.JoinGroup(userId, groupId);
            await userService.PromoteUser(userId, groupId);
            return Created(nameof(Get), groupId);
        }
    }
}
