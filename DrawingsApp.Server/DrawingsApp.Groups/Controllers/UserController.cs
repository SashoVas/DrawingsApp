using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.User;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrawingsApp.Groups.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService) 
            => this.userService = userService;

        [HttpPost]
        public async Task<ActionResult> JoinGroup(int groupId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await userService.UserExists(userId))
            {
                await userService.CreateUser(userId, User.Identity.Name);
            }
            await userService.JoinGroup(userId, groupId);
            return Created("",null);
        }
        [HttpPatch("AcceptUser")]
        public async Task<ActionResult> AcceptUser(UpdateUserInputModel input)
        {
            if (!await userService.IsAdmin(User.FindFirstValue(ClaimTypes.NameIdentifier),input.GroupId))
            {
                return Unauthorized();
            }
            await userService.AcceptUser(input.UserId, input.GroupId);
            return Ok();
        }
        [HttpPatch("PromoteUser")]
        public async Task<ActionResult> PromoteUser(UpdateUserInputModel input)
        {
            if (!await userService.IsAdmin(User.FindFirstValue(ClaimTypes.NameIdentifier), input.GroupId))
            {
                return Unauthorized();
            }
            await userService.PromoteUser(input.UserId, input.GroupId);
            return Ok();
        }
    }
}
