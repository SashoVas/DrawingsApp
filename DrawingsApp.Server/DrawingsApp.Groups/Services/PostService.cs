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
        private readonly IGroupService groupService;
        public PostService(DrawingsAppGroupsDbContext context, IUserService userService, IGroupService groupService)
        {
            this.context = context;
            this.userService = userService;
            this.groupService = groupService;
        }

        public async Task<int> CreatePost(string SenderId, string Title, int GroupId, string ImgUrl)
        {
            var post = new Post
            {
                GroupId=GroupId,
                SenderId= SenderId,
                Title=Title,
                ImgUrl=ImgUrl,
                PostedOn=DateTime.UtcNow
            };
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            return post.Id;
        }

        public  Task<List<PostOutputModel>> GetPosts(int groupId) 
            =>context.Posts
                .Where(p => p.GroupId == groupId)
                .OrderBy(p => p.PostedOn)
                .Take(10)
                .Select(p => new PostOutputModel
                {
                    PostedOn = p.PostedOn,
                    Id = p.Id,
                    ImgUrl = p.ImgUrl,
                    SenderUserName = p.Sender.Username,
                    Title = p.Title
                }).ToListAsync();

        public Task<List<PostOutputModel>> GetPostsByUser(string userId) 
            => context.Posts
                .Where(p => p.Group.UserGrops.Any(gu => gu.UserId == userId))
                .OrderBy(p => p.PostedOn)
                .Take(10)
                .Select(p => new PostOutputModel
                {
                    PostedOn = p.PostedOn,
                    Id = p.Id,
                    ImgUrl = p.ImgUrl,
                    SenderUserName = p.Sender.Username,
                    Title = p.Title
                }).ToListAsync();
    }
}
