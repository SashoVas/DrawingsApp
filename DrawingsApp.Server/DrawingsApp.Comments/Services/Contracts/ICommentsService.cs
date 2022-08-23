using DrawingsApp.Comments.Data.Models;

namespace DrawingsApp.Comments.Services.Contracts
{
    public interface ICommentsService
    {
        Task<Comment> CreateCommentOnPost(string userId,string userName,string postId,string contents);
        Task<Comment> CreateCommentOnComment(string userId,string userName,string postId,string contents,List<string>commentsPath);
    }
}
