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

        public async Task<int> CreatePost(string senderId, string title, int groupId, List<string> imgUrls,string outerId)
        {
            var post = new Post
            {
                GroupId=groupId,
                SenderId= senderId,
                OuterId=outerId,
                Title=title,
                Images=imgUrls.Select(i=>new Image {Url=i }).ToList(),
                PostedOn=DateTime.UtcNow
            };
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            return post.Id;
        }

        public async Task<bool> DeletePost(string postId)
        {
            var postData = await context.Posts
                .Where(p => p.OuterId == postId)
                .Include(p=>p.Likes)
                .FirstOrDefaultAsync();
    
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
                    Id = p.OuterId,
                    GroupName=p.Group.Title,
                    ImgUrls = p.Images.Select(i=>i.Url).ToList(),
                    SenderUserName = p.Sender.Username,
                    Title = p.Title,
                    Likes = p.Likes.Count(l => l.IsLike) - p.Likes.Count(l => !l.IsLike),
                    GroupId = p.GroupId
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
                    Id = p.OuterId,
                    GroupName=p.Group.Title,
                    ImgUrls = p.Images.Select(i => i.Url).ToList(),
                    SenderUserName = p.Sender.Username,
                    Title = p.Title,
                    Likes=p.Likes.Count(l=>l.IsLike)-p.Likes.Count(l=>!l.IsLike),
                    GroupId=p.GroupId
                }).ToListAsync();

        public async Task<int> LikePost(string userId,string postId, bool isLike)
        {
            var like =await context.Likes
                .Where(l => l.Post.OuterId == postId && l.UserId == userId)
                .FirstOrDefaultAsync();
            var post = await context.Posts
                .Where(p => p.OuterId == postId)
                .FirstOrDefaultAsync();
            int changeAmounth = 0;
            if (like is null)
            {
                changeAmounth = isLike?1:-1;
                await context.Likes.AddAsync(new PostUserLikes 
                { 
                    PostId=post.Id,
                    UserId=userId,
                    IsLike=isLike
                });
            }
            else
            {
                if (like.IsLike == isLike)
                {
                    changeAmounth = isLike ?-1:1;
                    context.Likes.Remove(like);
                }
                else
                {
                    changeAmounth = isLike ? 2 : -2;
                    like.IsLike = isLike;
                    context.Update(like);
                }
            }
            await context.SaveChangesAsync();
            return changeAmounth;
        }

        public async Task<bool> UpdatePost(  string outerId, string title)
        {
            var post = await context.Posts
                .Where(p => p.OuterId == outerId)
                .FirstOrDefaultAsync();
            post.Title = title;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
