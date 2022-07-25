using DrawingsApp.Groups.Models.OutputModels.Tag;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface ITagService
    {
        Task<int> Create(string name,string tagInfo);
        Task<string> GetTagInfo(int id);
        Task<bool> SetTagToGroup(string userId, int groupId,int tagId);
        Task<IEnumerable<TagOutputModel>> GetTags(string name);
    }
}
