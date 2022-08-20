using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Comments.Models.InputModels.Post;
using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Controllers;
using DrawingsApp.Messages.Post;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Comments.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostService postService;
        private readonly IUserRoleInGroupRepository userRoleInGroupRepo;
        private readonly IBus publisher;
        public PostController(IPostService postService, IUserRoleInGroupRepository userRoleInGroupRepo, IBus publisher)
        {
            this.postService = postService;
            this.userRoleInGroupRepo = userRoleInGroupRepo;
            this.publisher = publisher;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPost(string id)
        {
            var post = await postService.GetPost(id,GetUserId());
            if (post is null)
            {
                return NotFound();
            }
            post.Role = await userRoleInGroupRepo.GetRole(GetUserId(), post.Group.GroupId);
            if (post.Group.GroupType>=DrawingsApp.Data.Common.GroupType.Restricted )
            {
                if ((int)post.Role<1)
                {
                    return Unauthorized();
                }
            }
            return Ok(post);
        }
        [HttpPost]
        public async Task<ActionResult> CreatePost(CreatePostInputModel input)
        {
            if (input.ImgUrls.Count()>5)
            {
                return BadRequest();
            }
            if ((int)await userRoleInGroupRepo.GetRole(GetUserId(), input.GroupId) < 2)
            {
                return Unauthorized();
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
                return BadRequest();
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
                return BadRequest();
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
