using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Data.Common;

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
        Task<Role> GetRole(int groupId, string userId);
    }
}
