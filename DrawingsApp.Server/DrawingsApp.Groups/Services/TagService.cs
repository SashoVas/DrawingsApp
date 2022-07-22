using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Groups.Services
{
    public class TagService : ITagService
    {
        private readonly DrawingsAppGroupsDbContext dbContext;

        public TagService(DrawingsAppGroupsDbContext dbContext) 
            => this.dbContext = dbContext;

        public async Task<int> Create(string name, string tagInfo)
        {
            var tag = new Tag
            {
                TagName = name,
                TagInfo = tagInfo
            };
            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();
            return tag.Id;
        }

        public Task<string> GetTagInfo(int id) 
            => dbContext.Tags
                .Where(t => t.Id == id)
                .Select(t => t.TagInfo)
                .FirstOrDefaultAsync();

        public async Task<bool> SetTagToGroup(string userId, int groupId,int tagId)
        {
            await dbContext.GroupTag
                .AddAsync(new GroupTag
                {
                    GroupId = groupId,
                    TagId = tagId
                });
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
