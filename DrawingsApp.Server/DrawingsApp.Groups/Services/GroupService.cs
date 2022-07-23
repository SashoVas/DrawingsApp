using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Data.Models;
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
        public async Task<int> CreateGroup(string title,string moreInfo,GroupType groupType, List<int> tags)
        {
            var group = new Group 
            { 
                MoreInfo= moreInfo,
                Title= title,
                GroupType=groupType,
                GroupTags=tags.Select(t=>new GroupTag 
                {
                    TagId=t
                }).ToList()
            };

            await context.Groups.AddAsync(group);
            await context.SaveChangesAsync();
            return group.Id;
        }

        public Task<GroupOutputModel> GetGroup(int id) 
            => context.Groups.Where(g => g.Id == id)
                .Select(g => new GroupOutputModel
                {
                    MoreInfo = g.MoreInfo,
                    Title = g.Title,
                    Tags = g.GroupTags.Select(gt => gt.Tag.TagName).ToList()
                }).FirstOrDefaultAsync();

        public Task<GroupType> GetGroupType(int id) 
            => context.Groups
                .Where(g => g.Id == id)
                .Select(g => g.GroupType)
                .FirstOrDefaultAsync();
    }
}
