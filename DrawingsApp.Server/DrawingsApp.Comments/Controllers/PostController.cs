using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Comments.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostService postService;
        private readonly IUserRoleInGroupRepository userRoleInGroupRepo;
        public PostController(IPostService postService, IUserRoleInGroupRepository userRoleInGroupRepo)
        {
            this.postService = postService;
            this.userRoleInGroupRepo = userRoleInGroupRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPost(int id)
        {
            var post = await postService.GetPost(id);
            if (post is null)
            {
                return NotFound();
            }
            if (post.PostType==PostType.Private )
            {
                if ((int)(await userRoleInGroupRepo.GetRole(GetUserId(),post.GroupId))<1)
                {
                    return Unauthorized();
                }
            }
            return Ok(post);
        }
        //For Debuging
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPosts() 
            => Ok(await postService.GetPosts());
        [HttpDelete]
        public async Task<ActionResult>DeletePosts()
        {
            await postService.DeletePosts();
            return Ok();
        }
    }
}
