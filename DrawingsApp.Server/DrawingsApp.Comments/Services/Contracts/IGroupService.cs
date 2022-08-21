using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Comments.Services.Contracts
{
    public interface IGroupService
    {
        Task<GroupInfo> GetGroup(int groupId);
        Task<Role> GetRole(int groupId,string userId);
        Task CreateGroup(int groupId, string groupName, GroupType groupType, string userId);
        Task DeleteGroup(int groupId);
        Task UpdateGroup(int groupId, string groupName, GroupType groupType);
        Task AddUserToGroup(int groupId, string userId, Role groupRole);
        Task UpdateRole(int groupId, string userId, Role groupRole);
        Task RemoveRole(int groupId, string userId);

    }
}
