using DrawingsApp.Comments.Models.InputModels.Post;
using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Controllers;
using DrawingsApp.Data.Common;
using DrawingsApp.Messages.Post;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Comments.Controllers
{
    public class PostController : ApiController
    {
        private const int MaxImagesCount = 5;
        private readonly IPostService postService;
        private readonly IGroupService groupService;
        private readonly IBus publisher;
        public PostController(IPostService postService, IGroupService groupService, IBus publisher)
        {
            this.postService = postService;
            this.publisher = publisher;
            this.groupService = groupService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPost(string id)
        {
            var post = await postService.GetPost(id,GetUserId());
            if (post is null)
            {
                return NotFound("No such post");
            }
            if (post.Group.GroupType>=GroupType.Restricted && post.Role<Role.User)
            {
                return Unauthorized("Not Authorized");
            }
            return Ok(post);
        }
        [HttpPost]
        public async Task<ActionResult> CreatePost(CreatePostInputModel input)
        {
            if (input.ImgUrls.Count()> MaxImagesCount)
            {
                return BadRequest("Invalid input");
            }
            if (await groupService.GetRole(input.GroupId,GetUserId()) < Role.User)
            {
                return Unauthorized("Not Authorized");
            }
            var id=await postService.CreatePost(input.GroupId,input.Title,input.Description,GetUserId(),User.Identity.Name,input.ImgUrls);
            await publisher.Publish(new PostCreatedMessage
            {
                GroupId = input.GroupId,
                Images = input.ImgUrls,
                SenderId = GetUserId(),
                SenderName = User.Identity.Name,
                Id = id,
                PostedOn = DateTime.UtcNow,
                Title = input.Title,
                Description = input.Description
            });
            return CreatedAtAction(nameof(GetPost), new { id = id }, id);
        }
        [HttpPut]
        public async Task<ActionResult> UpdatePost(UpdatePostInputModel input)
        {
            if (!await postService.UpdatePost(input.PostId,input.Title,input.Description,GetUserId()))
            {
                return BadRequest("Invalid Input");
            }
            await publisher.Publish(new PostUpdateMessage 
            { 
                Id=input.PostId,
                Description=input.Description,
                Title=input.Title,
                UserId=GetUserId()
            });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(string id)
        {
            if (!await postService.DeletePost(id,GetUserId()))
            {
                return BadRequest("Invalid Input");
            }
            await publisher.Publish(new PostDeleteMessage
            {
                Id = id,
                UserId=GetUserId()
            });
            return Ok();
        }
        //For Debuging
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPosts() 
            => Ok(await postService.GetPosts());
        [HttpDelete("All")]
        public async Task<ActionResult>DeletePosts()
        {
            await postService.DeletePosts();
            return Ok();
        }
    }
}
