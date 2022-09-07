using DrawingsApp.Data.Common;
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

        public async Task<bool> EditProfile(string userId,string description, string imgUrl)
        {
            var user =await context.Users.FindAsync(userId);
            if (user is null)
            {
                return false;
            }
            user.Description = description ?? user.Description;
            user.ImgUrl = imgUrl ?? user.ImgUrl;
            context.Update(user);
            await context.SaveChangesAsync();
            return true;
        }

        public Task<ProfileOutputModel> GetCallerFullProfile(string userId)
        => context.Users
                .Where(u => u.Id == userId)
                .Select(u => new ProfileOutputModel
                {
                    UserId = u.Id,
                    UserName = u.Username,
                    Description = u.Description,
                    ImgUrl = u.ImgUrl,
                    Posts = u.Posts
                        .Select(p => new Models.OutputModels.Post.PostOutputModel
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
                    Groups = u.UserGrops
                        .Select(ug => new Models.OutputModels.Group.GroupListingOutputModel
                        {
                            Id = ug.GroupId,
                            ImgUrl = ug.Group.ImgUrl,
                            Title = ug.Group.Title,
                            IsJoined = true,
                        })
                }).FirstOrDefaultAsync();

        public Task<ProfileOutputModel> GetFullProfile(string userId,string callerId) 
            => context.Users
                .Where(u => u.Id == userId)
                .Select(u => new ProfileOutputModel
                {
                    UserId = u.Id,
                    UserName = u.Username,
                    Description=u.Description,
                    ImgUrl = u.ImgUrl,
                    Posts = u.Posts
                        .Where(p=>p.Group.GroupType==GroupType.Public||p.Group.UserGrops.Any(gu=>gu.UserId==callerId))
                        .Select(p => new Models.OutputModels.Post.PostOutputModel
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
                    Groups = u.UserGrops
                        .Select(ug => new Models.OutputModels.Group.GroupListingOutputModel
                        {
                            Id = ug.GroupId,
                            ImgUrl = ug.Group.ImgUrl,
                            Title = ug.Group.Title,
                            IsJoined = true,
                        })
                }).FirstOrDefaultAsync();

        public Task<ProfileInfoOutputModel> GetProfileInfo(string userId) 
            =>context.Users
                .Where(u => u.Id == userId)
                .Select(u => new ProfileInfoOutputModel
                {
                    UserId = u.Id,
                    Description = u.Description,
                    ImgUrl = u.ImgUrl,
                    UserName = u.Username
                })
                .FirstOrDefaultAsync();
    }
}
