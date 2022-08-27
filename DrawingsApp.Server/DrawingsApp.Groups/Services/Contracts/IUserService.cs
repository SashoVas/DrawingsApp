using DrawingsApp.Data.Common;
using DrawingsApp.Groups.Models.OutputModels.User;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> UserExists(string userId);
        Task<IEnumerable<UserOutputModel>> GetUsersByGroup(int groupId, Role role, bool lessUsers);
        Task<Role> JoinGroup(string userId, int groupId);
        Task CreateUser(string userId, string userName);
        Task AcceptUser(string userId, int groupId);
        Task PromoteUser(string userId, int groupId);
        Task<bool> IsAdmin(string userId, int groupId);
        Task<Role> GetRole(string userId, int groupId);
        Task<bool> LeaveGroup(string userId, int groupId);
        Task<UserRoleAndGroupTypeOutputModel> GetRoleAndGroupTypeAsync(string userId, int groupId);
        Task<bool> EnableNotifications(string userId,int groupId);
    }
}
