using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Models.InputModels;
using DrawingsApp.Groups.Models.OutputModels;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Groups.Services
{
    public class GroupService : IGroupService
    {
        private readonly DrawingsAppGroupsDbContext context;
        public GroupService(DrawingsAppGroupsDbContext context) 
            => this.context = context;
        public async Task<int> CreateGroup(string title,string moreInfo)
        {
            var group = new Group 
            { 
                MoreInfo= moreInfo,
                Title= title,
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
    }
}
