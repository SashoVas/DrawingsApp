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
        public PostController(
            IPostService postService,
            IUserService userService,
            IBus publisher)
        {
            this.postService = postService;
            this.userService = userService;
            this.publisher = publisher;
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
            return Ok(changeAmounth);
        }
    }
}
