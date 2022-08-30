using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Models.OutputModels.Profile;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Groups.Services
{
    public class ProfileService : IProfileService
    {
        private readonly DrawingsAppGroupsDbContext context;

        public ProfileService(DrawingsAppGroupsDbContext context)
        {
            this.context = context;
        }

        public Task<ProfileOutputModel> GetProfile(string userId) 
            => context.Users
            .Where(u => u.Id == userId)
            .Select(u => new ProfileOutputModel
            {
                UserId = u.Id,
                UserName = u.Username,
                Posts = u.Posts.Select(p => new Models.OutputModels.Post.PostOutputModel
                {
                    PostedOn = p.PostedOn.ToString("yyyy/MM/d"),
                    Id = p.OuterId,
                    GroupName = p.Group.Title,
                    ImgUrls = p.Images.Select(i => i.Url).ToList(),
                    SenderUserName = p.Sender.Username,
                    Title = p.Title,
                    Likes = p.Likes.Count(l => l.IsLike) - p.Likes.Count(l => !l.IsLike),
                    GroupId = p.GroupId,
                    GroupImgUrl = p.Group.ImgUrl
                }),
                Groups=u.UserGrops.Select(ug=>new Models.OutputModels.Group.GroupListingOutputModel
                {
                    Id=ug.GroupId,
                    ImgUrl=ug.Group.ImgUrl,
                    Title=ug.Group.Title,
                    IsJoined=true,
                })
            }).FirstOrDefaultAsync();
    }
}
