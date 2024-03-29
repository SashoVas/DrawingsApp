﻿using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.Group;
using DrawingsApp.Groups.Services.Contracts;
using DrawingsApp.Messages.Group;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    public class GroupController : ApiController
    {
        private const int MaxImagesCount= 5;
        private const int MaxTagsCount= 5;
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
                return NotFound("There is no such group");
            }
            return Ok(group);
        }
        [HttpGet]
        public async Task<ActionResult> Search([FromQuery]SearchGroupInputModel input)
            =>Ok(await groupService.Search(
                input.Name,
                input.Tags,
                input.UserId=="mine"?GetUserId():input.UserId,
                input.GroupType,
                input.Order,
                GetUserId(),
                input.Page));
        [HttpGet("User")]
        public async Task<ActionResult> GetGroupsByUser(bool isLess=false) 
            => Ok(await groupService.GetGropusByUser(GetUserId(),isLess));
        [HttpGet("Top")]
        public async Task<ActionResult> GetTopGroups(bool isLess = false)
            => Ok(await groupService.GetTopGroups(GetUserId(),isLess));

        [HttpPost]
        public async Task<ActionResult> Create(CreateGroupInputModel input)
        {
            if (input.Tags.Count() > MaxImagesCount || input.Tags.Count()> MaxTagsCount)
            {
                return BadRequest("Invalid input");
            }
            var userId = GetUserId();
            if (!await userService.UserExists(userId))
            {
                await userService.CreateUser(userId,User.Identity.Name);
            }
            var groupId = await groupService.CreateGroup(input.Title, input.MoreInfo,input.ImgUrl, input.GroupType, input.Tags);
            await userService.JoinGroup(userId, groupId);
            await userService.PromoteUser(userId, groupId);
            await publisher.Publish(new GroupCreateMessage
            {
                GroupId = groupId,
                GroupName=input.Title,
                GroupType=input.GroupType,
                UserId=GetUserId(),
                GroupImageUrl=input.ImgUrl
            });
            return CreatedAtAction(nameof(Get), new { id = groupId },groupId);
        }
        [HttpPut]
        public async Task<ActionResult> Update(UpdateGroupInputModel input)
        {
            if (!await userService.IsAdmin(GetUserId(),input.GroupId))
            {
                return Unauthorized("Not Authorized");
            }
            if (!await groupService.UpdateGroup(input.GroupId,input.Title,input.MoreInfo,input.ImgUrl,input.GroupType,input.Tags))
            {
                return BadRequest("Invalid Input");
            }
            await publisher.Publish(new GroupUpdateMessage
            {
                GroupId = input.GroupId,
                GroupName = input.Title,
                GroupType = input.GroupType,
                UserId = GetUserId()
            });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await userService.IsAdmin(GetUserId(), id))
            {
                return Unauthorized("Not Authorized");
            }
            await groupService.DeleteGroup(id);
            return Ok();
        }
    }
}
