using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Models.OutputModels.User;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> UserExists(string userId);
        Task<IEnumerable<UserOutputModel>> GetUsersByGroup(int groupId);
        Task JoinGroup(string userId, int groupId);
        Task CreateUser(string userId, string userName);
        Task AcceptUser(string userId, int groupId);
        Task PromoteUser(string userId, int groupId);
        Task<bool> IsAdmin(string userId,int groupId);
        Task<Role> GetRole(string userId,int groupId);
    }
}
