using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Comments.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostService postService;

        public PostController(IPostService postService) 
            => this.postService = postService;
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPost(int id) 
            => Ok(await postService.GetPost(id));
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPosts() 
            => Ok(await postService.GetPosts());
    }
}
