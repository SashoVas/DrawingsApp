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
                Images=imgUrls.Select(i=>new Image {Id=i }).ToList(),
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
                .Select(p => new 
                    { 
                        p.SenderId,
                        p.GroupId
                    })
                .FirstOrDefaultAsync();
            if (postData is null || postData.SenderId!=userId || !await userService.IsAdmin(userId, postData.GroupId))
            {
                return false;
            }
            var post = new Post
            {
                Id = postId
            };
            context.Posts.Remove(post);
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
                    PostedOn = p.PostedOn,
                    Id = p.Id,
                    ImgUrls = p.Images.Select(i=>i.Id).ToList(),
                    SenderUserName = p.Sender.Username,
                    Title = p.Title
                }).ToListAsync();

        public Task<List<PostOutputModel>> GetPostsByUser(string userId) 
            => context.Posts
                .Where(p => p.Group.UserGrops.Any(gu => gu.UserId == userId))
                .OrderByDescending(p => p.PostedOn)
                .Take(10)
                //.TakeLast(10)
                .Select(p => new PostOutputModel
                {
                    PostedOn = p.PostedOn,
                    Id = p.Id,
                    ImgUrls = p.Images.Select(i => i.Id).ToList(),
                    SenderUserName = p.Sender.Username,
                    Title = p.Title
                }).ToListAsync();

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
