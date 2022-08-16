using DrawingsApp.Controllers;
using DrawingsApp.Data.Common;
using DrawingsApp.Groups.Models.InputModels.User;
using DrawingsApp.Groups.Services.Contracts;
using DrawingsApp.Messages.User;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService userService;
        private readonly IBus publisher;
        public UserController(IUserService userService, IBus publisher)
        {
            this.userService = userService;
            this.publisher = publisher;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsers(int id, [FromQuery] Role role)
        {
            var groupTypeAndRole = await userService.GetRoleAndGroupTypeAsync(GetUserId(), id);
            if ((int)groupTypeAndRole.GrouptType > 0 && (int)groupTypeAndRole.Role < 2)
            {
                return Unauthorized();
            }
            return Ok(await userService.GetUsersByGroup(id, role));
        }

        [HttpPost("{groupId}")]
        public async Task<ActionResult> JoinGroup(int groupId)
        {
            var userId = GetUserId();
            if (!await userService.UserExists(userId))
            {
                await userService.CreateUser(userId, User.Identity.Name);
            }
            var role=await userService.JoinGroup(userId, groupId);
            await publisher.Publish(new PromoteUserRoleInGroupMessage
            {
                UserId=GetUserId(),
                GroupId=groupId,
                Role=role
            });
            return CreatedAtRoute(new{ 
                action="Get",
                controller="Group",
                id=groupId,
            }, null);
        }
        [HttpPut("AcceptUser")]
        public async Task<ActionResult> AcceptUser(UpdateUserInputModel input)
        {
            if (!await userService.IsAdmin(GetUserId(), input.GroupId))
            {
                return Unauthorized();
            }
            await userService.AcceptUser(input.UserId, input.GroupId);
            await publisher.Publish(new PromoteUserRoleInGroupMessage
            {
                UserId = GetUserId(),
                GroupId = input.GroupId,
                Role = Role.User
            });
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
            await publisher.Publish(new PromoteUserRoleInGroupMessage
            {
                UserId = GetUserId(),
                GroupId = input.GroupId,
                Role = Role.Admin
            });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> LeaveGroup(int id)
        {
            if (!await this.userService.LeaveGroup(GetUserId(), id))
            {
                return NotFound();
            }
            await publisher.Publish(new RemoveRoleFromUserMessage
            {
                GroupId=id,
                UserId=GetUserId()
            });
            return Ok();
        }
    }
}
