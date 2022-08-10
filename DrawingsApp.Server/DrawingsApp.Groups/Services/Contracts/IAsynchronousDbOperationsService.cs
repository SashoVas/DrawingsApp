using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IAsynchronousDbOperationsService
    {
        Task<(Role, GroupType)> GetRoleAndGroupTypeAsync(string userId,int groupId);
    }
}
