using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Models.InputModels.Group;
using DrawingsApp.Groups.Models.OutputModels.Group;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Groups.Services
{
    public class GroupService : IGroupService
    {
        private readonly DrawingsAppGroupsDbContext context;
        public GroupService(DrawingsAppGroupsDbContext context) 
            => this.context = context;
        public async Task<int> CreateGroup(string title,string moreInfo,string imgUrl,GroupType groupType, List<int> tags)
        {
            var group = new Group 
            { 
                MoreInfo= moreInfo,
                Title= title,
                GroupType=groupType,
                ImgUrl=imgUrl,
                GroupTags=tags.Select(t=>new GroupTag 
                {
                    TagId=t
                }).ToList()
            };

            await context.Groups.AddAsync(group);
            await context.SaveChangesAsync();
            return group.Id;
        }

        public async Task<bool> DeleteGroup(int groupId)
        {
            var group = new Group { Id = groupId };
            context.Remove(group);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<GroupListingOutputModel>> Search(string name, List<int>? tags, string? userId, GroupType? groupType, SortType orderType)
        {
            var query = context.Groups
                           .Where(g => g.Title.ToLower().StartsWith(name.ToLower()));
            if (!(userId is null))
            {
                query = query.Where(g => g.UserGrops.Any(ug => ug.UserId == userId));
            }
            if (!(groupType is null))
            {
                query = query.Where(g => g.GroupType == groupType);
            }
            if (!(tags is null))
            {
                foreach (var tag in tags)
                {
                    query = query.Where(g => g.GroupTags.Any(g=>g.TagId==tag));
                }
            }
            
            if (orderType!=SortType.None)
            {
                if (orderType==SortType.Ascending)
                {
                    query = query.OrderBy(ug => ug.UserGrops.Count());
                }
                else
                {
                    query = query.OrderByDescending(ug => ug.UserGrops.Count());
                }
            }
            return await query
                    .Take(10)
                    .Select(g => new GroupListingOutputModel
                    {
                        Id = g.Id,
                        ImgUrl = g.ImgUrl,
                        Title = g.Title,
                        Users = g.UserGrops.Count()
                    }).ToListAsync();
        }

        public async Task<IEnumerable<GroupListingOutputModel>> GetGropusByUser(string userId) 
            => await context.Groups
                .Where(g => g.UserGrops.Any(ug => ug.UserId == userId))
                .Select(g => new GroupListingOutputModel
                {
                    Id = g.Id,
                    ImgUrl=g.ImgUrl,
                    Title = g.Title,

                }).ToListAsync();

        public Task<GroupOutputModel> GetGroup(int id) 
            => context.Groups.Where(g => g.Id == id)
                .Select(g => new GroupOutputModel
                {
                    Id=g.Id,
                    MoreInfo = g.MoreInfo,
                    Title = g.Title,
                    ImgUrl=g.ImgUrl,
                    Users=g.UserGrops.Count(),
                    groupType=g.GroupType,
                    Tags = g.GroupTags.Select(gt => gt.Tag.TagName).ToList()
                }).FirstOrDefaultAsync();

        public Task<string> GetGroupName(int groupId) 
            => context.Groups
                .Where(g => g.Id == groupId)
                .Select(g => g.Title)
                .FirstOrDefaultAsync();

        public Task<GroupType> GetGroupType(int id) 
            => context.Groups
                .Where(g => g.Id == id)
                .Select(g => g.GroupType)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<GroupListingOutputModel>> GetTopGroups() 
            => await context.Groups
                .OrderBy(g => g.UserGrops.Count())
                .Take(10)
                .Select(g => new GroupListingOutputModel
                {
                    Id = g.Id,
                    ImgUrl = g.ImgUrl,
                    Title = g.Title
                }).ToListAsync();

        public async Task<bool> UpdateGroup(int groupId, string title, string moreInfo,string imgUrl, GroupType groupType, List<int> tags)
        {
            var group =await context.Groups
                .FindAsync(groupId);
            if (group is null)
            {
                return false;
            }
            group.Title = title;
            group.GroupType = groupType;
            group.MoreInfo = moreInfo;
            group.ImgUrl = imgUrl;
            try
            {
                await context.GroupTag.AddRangeAsync(tags.Select(t => new GroupTag
                {
                    GroupId = groupId,
                    TagId = t
                }));
            }
            catch (Exception)
            {
                return false;
            }
            context.Update(group);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
