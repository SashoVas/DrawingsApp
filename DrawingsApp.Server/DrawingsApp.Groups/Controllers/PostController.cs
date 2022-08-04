using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.Post;
using DrawingsApp.Groups.Models.OutputModels.Post;
using DrawingsApp.Groups.Services.Contracts;
using DrawingsApp.Messages.Post;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    public class PostController : ApiController
    {
        private readonly IBus publisher;
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IGroupService groupService;
        private readonly IAsynchronousDbOperationsService asynchronousDbOperationsService;
        public PostController(
            IPostService postService,
            IUserService userService,
            IAsynchronousDbOperationsService asynchronousDbOperationsService,
            IBus publisher,
            IGroupService groupService)
        {
            this.postService = postService;
            this.userService = userService;
            this.asynchronousDbOperationsService = asynchronousDbOperationsService;
            this.publisher = publisher;
            this.groupService = groupService;
        }

        [HttpGet("Group/{id}")]
        public async Task<ActionResult<IEnumerable<PostOutputModel>>> GetPostsByGroup(int id)
        {
            var (role, groupType) =await asynchronousDbOperationsService.GetRoleAndGroupTypeAsync(GetUserId(), id);
            if (Math.Max(1,(int)role) <(int)groupType)
            {
                return Unauthorized();
            }
            return Ok(await postService.GetPosts(id));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostOutputModel>>> GetPostsByUser() 
            => Ok(await postService.GetPostsByUser(GetUserId()));
        [HttpPost]
        public async Task<ActionResult> CreatePost(CreatePostInputModel input)
        {
            if (input.ImgUrls.Count()>5)
            {
                return BadRequest();
            }
            if ((int)(await userService.GetRole(GetUserId(),input.GroupId))<2)
            {
                return Unauthorized();
            }
            var id = await postService.CreatePost(GetUserId(), input.Title, input.GroupId, input.ImgUrls);
            await publisher.Publish(new PostCreatedMessage 
            {
                GroupId=input.GroupId,
                GroupName=await groupService.GetGroupName(input.GroupId),
                Images=input.ImgUrls,
                SenderId=GetUserId(),
                SenderName=User.Identity.Name,
                Id=id,
                PostedOn=DateTime.UtcNow,
                Title=input.Title,
                Description=input.Description
            });
            return Created("",id);
        }
        [HttpPut]
        public async Task<ActionResult> UpdatePost(UpdatePostInputModel input)
        {
            if (!await postService.UpdatePost(GetUserId(), input.PostId, input.Title))
            {
                return Unauthorized();
            }
            await publisher.Publish(new PostUpdateMessage
            {
                Id = input.PostId,
                Description = input.Description,
                Title = input.Title
            });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if (!await postService.DeletePost(GetUserId(),id))
            {
                return Unauthorized();
            }
            await publisher.Publish(new PostDeleteMessage
            {
                Id=id
            });
            return Ok();
        }
    }
}
