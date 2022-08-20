using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Comments.Models.OutputModels.Post;
using DrawingsApp.Comments.Models.OutputModels.User;
using DrawingsApp.Comments.Services.Contracts;

namespace DrawingsApp.Comments.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repo;
        private readonly IUserRoleInGroupRepository roleRepo;
        private readonly IGroupRepository groupRepo;
        public PostService(IPostRepository repo, IUserRoleInGroupRepository roleRepo, IGroupRepository groupRepo)
        {
            this.repo = repo;
            this.roleRepo = roleRepo;
            this.groupRepo = groupRepo;
        }

        public async Task<string> CreatePost(
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
                Sender=new SenderInfo
                {
                    SenderId=senderId,
                    SenderName=senderName
                },
                Description = description,
                Group=await groupRepo.GetGroup(groupId),
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
            if (post.Sender.SenderId == userId || (int)await roleRepo.GetRole(userId, post.Group.GroupId) == 3)
            {
                post.IsDeleated = true;
                await repo.UpdatePost(post);
                return true;
            }
            return false;
        }

        public async Task DeletePosts() 
            => await repo.DeletePosts();

        public async Task<PostOutputModel> GetPost(string id,string userId)
        {
            var post = await repo.GetPost(id);
            if (post is null ||post.IsDeleated)
            {
                return null;
            }
            return new PostOutputModel
            {
                Id=post.Id,
                ImgUrls = post.ImgUrls,
                Description = post.Description,
                User=new UserOutputModel
                {
                    UserName = post.Sender.SenderName,
                    UserId=post.Sender.SenderId
                },
                Comments = post.Comments,
                IsMe=post.Sender.SenderId==userId,
                Group=new Models.OutputModels.Group.GroupOutputModel
                {
                    GroupId=post.Group.GroupId,
                    GroupName = post.Group.GroupName,
                    GroupType=post.Group.GroupType
                },
                Likes = post.Likes,
                PostedOn = post.PostedOn.ToString("yyyy,MM,dd"),
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
            if(post.Sender.SenderId==userId ||(int)await roleRepo.GetRole(userId,post.Group.GroupId)==3)
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
