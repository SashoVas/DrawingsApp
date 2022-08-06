using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Models.OutputModels.Tag;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Groups.Services
{
    public class TagService : ITagService
    {
        private readonly DrawingsAppGroupsDbContext context;

        public TagService(DrawingsAppGroupsDbContext dbContext) 
            => this.context = dbContext;

        public async Task<int> Create(string name, string tagInfo)
        {
            var tag = new Tag
            {
                TagName = name,
                TagInfo = tagInfo
            };
            await context.Tags.AddAsync(tag);
            await context.SaveChangesAsync();
            return tag.Id;
        }

        public Task<string> GetTagInfo(int id) 
            => context.Tags
                .Where(t => t.Id == id)
                .Select(t => t.TagInfo)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TagOutputModel>> GetTags(string name) 
            =>await context.Tags
                .Where(t => t.TagName.StartsWith(name??""))
                .Select(t => new TagOutputModel
                {
                    TagId = t.Id,
                    TagInfo = t.TagInfo,
                    TagName = t.TagName
                }).ToListAsync();

        public async Task<bool> SetTagToGroup(string userId, int groupId,int tagId)
        {
            await context.GroupTag
                .AddAsync(new GroupTag
                {
                    GroupId = groupId,
                    TagId = tagId
                });
            await context.SaveChangesAsync();
            return true;
        }
    }
}
