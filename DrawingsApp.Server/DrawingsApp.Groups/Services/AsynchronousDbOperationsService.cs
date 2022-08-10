using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Groups.Services
{
    public class AsynchronousDbOperationsService : IAsynchronousDbOperationsService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public AsynchronousDbOperationsService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task<(Role, GroupType)> GetRoleAndGroupTypeAsync(string userId, int groupId)
        {
            var role = GetRole(userId, groupId);
            var groupType = GetGroupType(groupId);
            await Task.WhenAll(role,groupType);
            return (role.Result, groupType.Result);
        }

        private async Task<Role> GetRole(string userId, int groupId)
        {
            var context = scopeFactory.CreateScope().ServiceProvider.GetService<DrawingsAppGroupsDbContext>();
            return await context.UserGroups
                .Where(g => g.UserId == userId && g.GroupId == groupId)
                .Select(ug => ug.Role)
                .FirstOrDefaultAsync();
        }
        private async Task<GroupType> GetGroupType(int groupId)
        {
            var context = scopeFactory.CreateScope().ServiceProvider.GetService<DrawingsAppGroupsDbContext>();
            return await context.Groups
                .Where(g => g.Id == groupId)
                .Select(ug => ug.GroupType)
                .FirstOrDefaultAsync();
        }
    }
}
