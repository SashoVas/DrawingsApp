using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Comments.Models.OutputModels.Post;
using DrawingsApp.Comments.Services.Contracts;

namespace DrawingsApp.Comments.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repo;
        private readonly IUserRoleInGroupRepository roleRepo;
        public PostService(IPostRepository repo, IUserRoleInGroupRepository roleRepo)
        {
            this.repo = repo;
            this.roleRepo = roleRepo;
        }

        public async Task<string> CreatePost(
            string groupName,
            int groupId,
            string title,
            string description,
            string senderId,
            string senderName,
            ICollection<string> ImgUrls)
        {
            var post = new Post
            {
                PostedOn = DateTime.UtcNow,
                SenderId = senderId,
                SenderName = senderName,
                Description = description,
                GroupId = groupId,
                GroupName = groupName,
                Title = title,
                ImgUrls = ImgUrls,
            };
            await repo.CreatePost(post);
            return post.Id;
        }

        public async Task<bool> DeletePost(string postId,string userId)
        {
            var post = await repo.GetPost(postId);
            if (post is null)
            {
                return false;
            }
            if (post.SenderId == userId || (int)await roleRepo.GetRole(userId, post.GroupId) == 3)
            {
                post.IsDeleated = true;
                await repo.UpdatePost(post);
                return true;
            }
            return false;
        }

        public async Task DeletePosts() 
            => await repo.DeletePosts();

        public async Task<PostOutputModel> GetPost(string id)
        {
            var post = await repo.GetPost(id);
            if (post is null)
            {
                return null;
            }
            return new PostOutputModel
            {
                Id=post.Id,
                ImgUrls = post.ImgUrls,
                Description = post.Description,
                SenderId = post.SenderId,
                Comments = post.Comments,
                GroupId = post.GroupId,
                GroupName = post.GroupName,
                Likes = post.Likes,
                PostedOn = post.PostedOn.ToString("yyyy,MM,dd"),
                SenderName = post.SenderName,
                Title = post.Title,
            };
        }

        public Task<IEnumerable<Post>> GetPosts() => repo.GetPosts();

        public async Task LikePost(string postId, int changeAmounth)
        {
            var post =await repo.GetPost(postId);
            post.Likes += changeAmounth;
            await repo.UpdatePost(post);
        }

        public async Task<bool> UpdatePost(string postId, string title,string description,string userId)
        {
            var post = await repo.GetPost(postId);
            if (post is null)
            {
                return false;
            }
            if(post.SenderId==userId ||(int)await roleRepo.GetRole(userId,post.GroupId)==3)
            {
                post.Title = title;
                post.Description = description;
                await repo.UpdatePost(post);
                return true;
            }
            return false;
        }
    }
}
