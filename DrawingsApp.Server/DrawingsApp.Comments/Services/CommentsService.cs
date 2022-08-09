using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Comments.Services.Contracts;

namespace DrawingsApp.Comments.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IPostRepository repo;

        public CommentsService(IPostRepository repo) 
            => this.repo = repo;

        private Comment GetComment(ICommentable parent, string id) 
            => parent.Comments
                .FirstOrDefault(p => p.Id == id);
        public async Task<bool> CreateCommentOnComment(string userId, string userName, int postId, string contents, List<string> commentsPath)
        {
            var post = await repo.GetPost(postId);
            var comment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                Contents = contents,
                PostId = postId,
                UserName = userName,
                UserId = userId,
            };
            ICommentable current = post;
            foreach (var id in commentsPath)
            {
                current = GetComment(current, id);
                if (current is null)
                {
                    return false;
                }
            }
            current.Comments.Add(comment);
            await repo.UpdatePost(post);
            return true;
        }

        public async Task<bool> CreateCommentOnPost(string userId, string userName, int postId, string contents)
        {
            var post =await repo.GetPost(postId);
            if (post is null)
            {
                return false;
            }
            var comment = new Comment
            {
                Id=Guid.NewGuid().ToString(),
                Contents=contents,
                PostId=postId,
                UserName=userName,
                UserId=userId,
            };
            post.Comments.Add(comment);
            await repo.UpdatePost(post);
            return true;
        }
    }
}
