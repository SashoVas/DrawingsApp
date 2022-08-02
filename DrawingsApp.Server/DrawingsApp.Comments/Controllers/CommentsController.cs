using DrawingsApp.Comments.Models.InputModels.Comments;
using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Comments.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost("Post")]
        public async Task<ActionResult> CreateCommentOnPost(CreateCommentOnPostInputModel input)
        {
            if (!await commentsService.CreateCommentOnPost(GetUserId(), User.Identity.Name, input.PostId, input.Contents))
            {
                return NotFound();
            }
            return Created("",null);
        }
        [HttpPost("Comment")]
        public async Task<ActionResult> CreateCommentOnComment(CreateCommentOnCommentInputModel input)
        {
            if (!await commentsService.CreateCommentOnComment(GetUserId(), User.Identity.Name, input.PostId, input.Contents, input.CommentsPath))
            {
                return NotFound();
            }
            return Created("", null);
        }

    }
}
