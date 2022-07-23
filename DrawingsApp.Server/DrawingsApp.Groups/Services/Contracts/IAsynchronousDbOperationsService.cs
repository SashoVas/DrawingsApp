using DrawingsApp.Groups.Data.Models;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IAsynchronousDbOperationsService
    {
        Task<(Role, GroupType)> GetRoleAndGroupTypeAsync(string userId,int groupId);
    }
}
