using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Comments.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IPostRepository repo;
        private readonly IUserRoleInGroupRepository roles;
        public CommentsService(IPostRepository repo, IUserRoleInGroupRepository roles)
        {
            this.repo = repo;
            this.roles = roles;
        }
        private async Task<bool> ValidateValuesWhenCreatingComment(Post post,string userId)
        {
            if (post is null)
            {
                return false;
            }
            if ((int)await roles.GetRole(userId, post.Group.GroupId) < (int)Role.User)
            {
                throw new UnauthorizedAccessException();
            }
            return true;
        }
        private Comment GetComment(ICommentable parent, string id) 
            => parent.Comments
                .FirstOrDefault(p => p.Id == id);
        public async Task<bool> CreateCommentOnComment(string userId, string userName, string postId, string contents, List<string> commentsPath)
        {
            var post = await repo.GetPost(postId);
            if (!await ValidateValuesWhenCreatingComment(post, userId))
            {
                return false;
            }
            var comment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                Contents = contents,
                PostId = postId,
                Sender=new SenderInfo
                {
                    SenderName = userName,
                    SenderId = userId,
                }
                
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

        public async Task<bool> CreateCommentOnPost(string userId, string userName, string postId, string contents)
        {
            var post =await repo.GetPost(postId);
            if (!await ValidateValuesWhenCreatingComment(post, userId))
            {
                return false;
            }
            var comment = new Comment
            {
                Id=Guid.NewGuid().ToString(),
                Contents=contents,
                PostId=postId,
                Sender = new SenderInfo
                {
                    SenderName = userName,
                    SenderId = userId,
                }
            };
            post.Comments.Add(comment);
            await repo.UpdatePost(post);
            return true;
        }
    }
}
