using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.User;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService) 
            => this.userService = userService;
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsers(int id) 
            => Ok(await userService.GetUsersByGroup(id));

        [HttpPost]
        public async Task<ActionResult> JoinGroup(int groupId)
        {
            var userId = GetUserId();
            if (!await userService.UserExists(userId))
            {
                await userService.CreateUser(userId, User.Identity.Name);
            }
            await userService.JoinGroup(userId, groupId);
            return Created("",null);
        }
        [HttpPut("AcceptUser")]
        public async Task<ActionResult> AcceptUser(UpdateUserInputModel input)
        {
            if (!await userService.IsAdmin(GetUserId(), input.GroupId))
            {
                return Unauthorized();
            }
            await userService.AcceptUser(input.UserId, input.GroupId);
            return Ok();
        }
        [HttpPut("PromoteUser")]
        public async Task<ActionResult> PromoteUser(UpdateUserInputModel input)
        {
            if (!await userService.IsAdmin(GetUserId(), input.GroupId))
            {
                return Unauthorized();
            }
            await userService.PromoteUser(input.UserId, input.GroupId);
            return Ok();
        }
    }
}
