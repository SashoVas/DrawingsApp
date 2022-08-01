using DrawingsApp.Comments.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Comments.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService postService) 
            => this.postService = postService;
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPost(int id) 
            => Ok(await postService.GetPost(id));

    }
}
