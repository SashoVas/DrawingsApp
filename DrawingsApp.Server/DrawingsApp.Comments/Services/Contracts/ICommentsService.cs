namespace DrawingsApp.Comments.Services.Contracts
{
    public interface ICommentsService
    {
        Task<bool> CreateCommentOnPost(string userId,string userName,int postId,string contents);
        Task<bool> CreateCommentOnComment(string userId,string userName,int postId,string contents,List<string>commentsPath);
    }
}
