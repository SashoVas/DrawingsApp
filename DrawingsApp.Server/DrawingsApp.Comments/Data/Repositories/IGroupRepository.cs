using DrawingsApp.Comments.Data.Models;

namespace DrawingsApp.Comments.Data.Repositories
{
    public interface IGroupRepository
    {
        Task<GroupInfo> GetGroup(int groupId);
        Task<IEnumerable<GroupInfo>> GetAllGroups();
        Task CreateGroup(GroupInfo group);
        Task UpdateGroup(GroupInfo group);
        Task DeleteAllGroups();
        Task DeleteGroup(int groupId);
    }
}
