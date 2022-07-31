using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.Post;
using DrawingsApp.Groups.Models.OutputModels.Post;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IAsynchronousDbOperationsService asynchronousDbOperationsService;
        public PostController(IPostService postService, IUserService userService, IAsynchronousDbOperationsService asynchronousDbOperationsService)
        {
            this.postService = postService;
            this.userService = userService;
            this.asynchronousDbOperationsService = asynchronousDbOperationsService;
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
            return Created("", await postService.CreatePost(GetUserId(), input.Title, input.GroupId, input.ImgUrls));
        }
        [HttpPut]
        public async Task<ActionResult> UpdatePost(UpdatePostInputModel input)
        {
            if (!await postService.UpdatePost(GetUserId(), input.PostId, input.Title))
            {
                return Unauthorized();
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if (!await postService.DeletePost(GetUserId(),id))
            {
                return Unauthorized();
            }
            return Ok();
        }
    }
}
