using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Models.OutputModels.Post;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Groups.Services
{
    public class PostService : IPostService
    {
        private readonly DrawingsAppGroupsDbContext context;
        private readonly IUserService userService;
        public PostService(DrawingsAppGroupsDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<int> CreatePost(string senderId, string title, int groupId, List<string> imgUrls)
        {
            var post = new Post
            {
                GroupId=groupId,
                SenderId= senderId,
                Title=title,
                Images=imgUrls.Select(i=>new Image {Url=i }).ToList(),
                PostedOn=DateTime.UtcNow
            };
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            return post.Id;
        }

        public async Task<bool> DeletePost(string userId,int postId)
        {
            var postData = await context.Posts
                .Where(p => p.Id == postId)
                .Include(p=>p.Likes)
                .FirstOrDefaultAsync();
            if (postData is null || postData.SenderId!=userId || !await userService.IsAdmin(userId, postData.GroupId))
            {
                return false;
            }

            context.Likes.RemoveRange(postData.Likes);
            context.Posts.Remove(postData);
            await context.SaveChangesAsync();
            return true;
        }

        public  Task<List<PostOutputModel>> GetPosts(int groupId) 
            =>context.Posts
                .Where(p => p.GroupId == groupId)
                .OrderByDescending(p => p.PostedOn)
                .Take(10)
                //.TakeLast(10)
                .Select(p => new PostOutputModel
                {
                    PostedOn = p.PostedOn.ToString("yyyy/MM/d"),
                    Id = p.Id,
                    GroupName=p.Group.Title,
                    ImgUrls = p.Images.Select(i=>i.Url).ToList(),
                    SenderUserName = p.Sender.Username,
                    Title = p.Title,
                    Likes=p.Likes.Count(),
                    GroupId=p.GroupId
                }).ToListAsync();

        public Task<List<PostOutputModel>> GetPostsByUser(string userId) 
            => context.Posts
                .Where(p => p.Group.UserGrops
                    .Any(gu => gu.UserId == userId && (int)gu.Role>1))
                .OrderByDescending(p => p.PostedOn)
                .Take(10)
                //.TakeLast(10)
                .Select(p => new PostOutputModel
                {
                    PostedOn = p.PostedOn.ToString("yyyy/MM/d"),
                    Id = p.Id,
                    GroupName=p.Group.Title,
                    ImgUrls = p.Images.Select(i => i.Url).ToList(),
                    SenderUserName = p.Sender.Username,
                    Title = p.Title,
                    Likes=p.Likes.Count(),
                    GroupId=p.GroupId
                }).ToListAsync();

        public async Task<bool> LikePost(string userId,int postId)
        {
            var like =await context.Likes
                .Where(l => l.PostId == postId && l.UserId == userId)
                .FirstOrDefaultAsync();
            bool isNewLike = false;
            if (like is null)
            {
                isNewLike = true;
                await context.Likes.AddAsync(new PostUserLikes 
                { 
                    PostId=postId,
                    UserId=userId
                });
            }
            else
            {
                context.Likes.Remove(like);
            }
            await context.SaveChangesAsync();
            return isNewLike;
        }

        public async Task<bool> UpdatePost(string senderId, int postId, string title)
        {
            var post =await context.Posts
                .FindAsync(postId);
            if (post.SenderId!=senderId)
            {
                return false;
            }
            post.Title = title;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
