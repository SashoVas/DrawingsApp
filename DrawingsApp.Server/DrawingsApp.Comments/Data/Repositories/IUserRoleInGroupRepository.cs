using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Comments.Data.Repositories
{
    public interface IUserRoleInGroupRepository
    {
        Task<Role> GetRole(string userId,int groupId);
        Task UpdateRole(UserRoleInGroup role);
        Task<IEnumerable<UserRoleInGroup>> GetUserRoles();
        Task RemoveAll();
        Task RemoveOne(string userId, int groupId);
    }
}
