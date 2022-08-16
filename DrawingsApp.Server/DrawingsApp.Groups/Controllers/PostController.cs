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
        public PostController(
            IPostService postService,
            IUserService userService,
            IBus publisher,
            IGroupService groupService)
        {
            this.postService = postService;
            this.userService = userService;
            this.publisher = publisher;
            this.groupService = groupService;
        }

        [HttpGet("Group/{id}")]
        public async Task<ActionResult<IEnumerable<PostOutputModel>>> GetPostsByGroup(int id)
        {
            var groupTypeAndRole =await userService.GetRoleAndGroupTypeAsync(GetUserId(), id);
            if ((int)groupTypeAndRole.GrouptType>0&& (int)groupTypeAndRole.Role<2)
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
            var postCreateData = await groupService.GetGroupDataForNewPost(input.GroupId);
            await publisher.Publish(new PostCreatedMessage 
            {
                GroupId=input.GroupId,
                GroupName=postCreateData.GroupName,
                Images=input.ImgUrls,
                SenderId=GetUserId(),
                SenderName=User.Identity.Name,
                Id=id,
                PostedOn=DateTime.UtcNow,
                Title=input.Title,
                Description=input.Description,
                PostType=postCreateData.PostType
            });
            return Created("https://localhost:7013/Comments/"+id, id);
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
        [HttpPut("Like")]
        public async Task<ActionResult>LikePost(LikePostInputModel input)
        {
            if ((int)(await userService.GetRole(GetUserId(), input.GroupId)) < 2)
            {
                return Unauthorized();
            }
            var changeAmounth = await postService.LikePost(GetUserId(), input.PostId,input.IsLike);

            await publisher.Publish(new PostLikeMessage 
            { 
                GroupId=input.GroupId,
                PostId=input.PostId,
                UserId=GetUserId(),
                UserName=User.Identity.Name,
                ChangeAmounth= changeAmounth
            });
            return Ok();
        }
    }
}
