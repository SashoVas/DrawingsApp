using DrawingsApp.Comments.Data;
using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Services.Contracts;

namespace DrawingsApp.Comments.Services
{
    public class PostService : IPostService
    {
        private readonly MongoDbCommentsRepository repo;

        public PostService(MongoDbCommentsRepository repo) 
            => this.repo = repo;

        public async Task<bool> CreatePost(
            int outerId,
            int groupId,
            string groupName,
            string title,
            string explanation,
            string senderId,
            string senderName)
        {
            await repo.CreatePost(new Post
            {
                PostedOn = DateTime.UtcNow,
                SenderId = senderId,
                SenderName =senderName,
                Explanation = explanation,
                GroupId = groupId,
                OuterId = outerId,
                GroupName = groupName,
                Title = title
            }) ;
            return true;
        }
        public Task<Post> GetPost(int id) => repo.GetPost(id);
    }
}
