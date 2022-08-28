using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Comments.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IPostRepository repo;
        private readonly IGroupService groupService;
        public CommentsService(IPostRepository repo, IGroupService groupService)
        {
            this.repo = repo;
            this.groupService = groupService;
        }
        private async Task<bool> ValidateValuesWhenCreatingComment(Post post,string userId)
        {
            if (post is null)
            {
                return false;
            }
            if (await groupService.GetRole(post.GroupId,userId) < Role.User)
            {
                throw new UnauthorizedAccessException("Not Authorized");
            }
            return true;
        }
        private void AddReplyToComment(ICommentable current,List<string> commentsPath,Comment comment)
        {
            foreach (var id in commentsPath)
            {
                current = GetComment(current, id);
                if (current is null)
                {
                    throw new ArgumentException("Invalid Input");
                }
            }
            current.Comments.Add(comment);
        }
        private Comment GetComment(ICommentable parent, string id) 
            => parent.Comments
                .FirstOrDefault(p => p.Id == id);
        public async Task<Comment> CreateCommentOnComment(string userId, string userName, string postId, string contents, List<string> commentsPath)
        {
            var post = await repo.GetPost(postId);
            if (!await ValidateValuesWhenCreatingComment(post, userId))
            {
                throw new ArgumentException("Invalid Input");
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
            AddReplyToComment(post,commentsPath,comment);
            await repo.UpdatePost(post);
            return comment;
        }

        public async Task<Comment> CreateCommentOnPost(string userId, string userName, string postId, string contents)
        {
            var post =await repo.GetPost(postId);
            if (!await ValidateValuesWhenCreatingComment(post, userId))
            {
                throw new ArgumentException("Invalid Input");
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
            return comment;
        }
    }
}
