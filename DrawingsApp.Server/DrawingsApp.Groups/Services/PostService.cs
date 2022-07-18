using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Models.OutputModels.Post;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Groups.Services
{
    public class PostService : IPostService
    {
        private readonly DrawingsAppGroupsDbContext context;

        public PostService(DrawingsAppGroupsDbContext context) 
            => this.context = context;

        public Task<List<PostOutputModel>> GetPosts(int groupId) 
            => context.Posts
                .Where(p => p.GroupId == groupId)
                .OrderBy(p => p.PostedOn)
                .Take(10)
                .Select(p => new PostOutputModel
                {
                    PostedOn = p.PostedOn,
                    Id = p.Id,
                    ImgUrl = p.ImgUrl,
                    SenderUserName = p.SenderUserName,
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
                    SenderUserName = p.SenderUserName,
                    Title = p.Title
                }).ToListAsync();
    }
}
