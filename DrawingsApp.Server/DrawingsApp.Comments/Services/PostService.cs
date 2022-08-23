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
        private readonly IGroupService groupService;
        public PostService(IPostRepository repo, IGroupService groupService)
        {
            this.repo = repo;
            this.groupService = groupService;
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
                GroupId=groupId,
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
            if (post.Sender.SenderId == userId || (int)await groupService.GetRole(post.GroupId, userId) == 3)
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
            var group = await groupService.GetGroup(post.GroupId);
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
                    GroupId=group.GroupId,
                    GroupName = group.GroupName,
                    GroupType=group.GroupType,
                    GroupImgUrl=group.GroupImgUrl
                },
                Likes = post.Likes,
                PostedOn = post.PostedOn.ToString("yyyy,MM,dd"),
                Title = post.Title,
                Role=group.Users
                .Where(u=>u.UserId==userId)
                .Select(u=>u.Role)
                .FirstOrDefault()
                
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
            if (post is null || post.IsDeleated)
            {
                return false;
            }
            if(post.Sender.SenderId==userId ||(int)await groupService.GetRole(post.GroupId, userId) ==3)
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
