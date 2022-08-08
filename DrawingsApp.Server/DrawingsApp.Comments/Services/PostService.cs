using DrawingsApp.Comments.Data;
using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Models.OutputModels.Post;
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
            string description,
            string senderId,
            string senderName,
            ICollection<string> ImgUrls)
        {
            await repo.CreatePost(new Post
            {
                PostedOn = DateTime.UtcNow,
                SenderId = senderId,
                SenderName =senderName,
                Description = description,
                GroupId = groupId,
                OuterId = outerId,
                GroupName = groupName,
                Title = title,
                ImgUrls=ImgUrls
            }) ;
            return true;
        }

        public async Task DeletePost(int outerid)
        {
            var post = await repo.GetPost(outerid);
            post.IsDeleated = true;
            await repo.UpdatePost(post);
        }

        public async Task<PostOutputModel> GetPost(int id)
        {
            var post = await repo.GetPost(id);
            return new PostOutputModel
            {
                ImgUrls = post.ImgUrls,
                Description = post.Description,
                SenderId = post.SenderId,
                Comments = post.Comments,
                GroupId = post.GroupId,
                GroupName = post.GroupName,
                Likes = post.Likes,
                OuterId = post.OuterId,
                PostedOn = post.PostedOn.ToString("yyyy,MM,dd"),
                SenderName = post.SenderName,
                Title = post.Title
            };
        }

        public Task<IEnumerable<Post>> GetPosts() => repo.GetPosts();

        public async Task LikePost(int outerId, bool isNewLike)
        {
            var post =await repo.GetPost(outerId);
            if (isNewLike)
            {
                post.Likes++;
            }
            else
            {
                post.Likes--;
            }
            await repo.UpdatePost(post);
        }

        public async Task UpdatePost(int outerId,string title,string description)
        {
            var post = await repo.GetPost(outerId);
            post.Title = title;
            post.Description = description;
            await repo.UpdatePost(post);
        }
    }
}
