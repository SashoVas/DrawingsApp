﻿using DrawingsApp.Controllers;
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
        [HttpGet("Role/{id}")]
        public async Task<ActionResult> GetRoleInGroup(int id) 
            => Ok(await this.userService.GetRole(GetUserId(), id));
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsers(int id, [FromQuery] Role role,[FromQuery] bool lessUsers =false)
        {
            var groupTypeAndRole = await userService.GetRoleAndGroupTypeAsync(GetUserId(), id);
            if (groupTypeAndRole.GrouptType > GroupType.Public && groupTypeAndRole.Role < Role.User)
            {
                return Unauthorized("Not Authorized");
            }
            return Ok(await userService.GetUsersByGroup(id, role,lessUsers));
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
            await publisher.Publish(new CreateUserRoleMessage
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
        [HttpPut("Notifications/{id}")]
        public async Task<ActionResult> Notifications(int id)
        {
            if (!await userService.EnableNotifications(GetUserId(),id))
            {
                return NotFound("User not in group");
            }
            await publisher.Publish(new EnableNotificationsMessage
            {
                GroupId=id,
                UserId=GetUserId()
            });
            return Ok();
        }
        [HttpPut("AcceptUser")]
        public async Task<ActionResult> AcceptUser(UpdateUserInputModel input)
        {
            if (!await userService.IsAdmin(GetUserId(), input.GroupId))
            {
                return Unauthorized("Not Authorized");
            }
            await userService.AcceptUser(input.UserId, input.GroupId);
            await publisher.Publish(new PromoteUserRoleInGroupMessage
            {
                UserId = input.UserId,
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
                return Unauthorized("Not Authorized");
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
                return NotFound("No such user in this group");
            }
            await publisher.Publish(new RemoveRoleFromUserMessage
            {
                GroupId=id,
                UserId=GetUserId()
            });
            return Ok();
        }
        [HttpDelete("Kick")]
        public async Task<ActionResult> KickFromGroup(int groupId,string userId)
        {
            if (!await this.userService.IsAdmin(GetUserId(),groupId))
            {
                return Unauthorized("Not Authorized");
            }
            if (!await this.userService.LeaveGroup(userId, groupId))
            {
                return NotFound("No such user in the group");
            }
            await publisher.Publish(new RemoveRoleFromUserMessage
            {
                GroupId = groupId,
                UserId = userId
            });
            return Ok();
        }
    }
}
