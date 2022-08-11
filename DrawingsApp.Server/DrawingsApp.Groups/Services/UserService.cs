using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Models.OutputModels.User;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Groups.Services
{
    public class UserService : IUserService
    {
        private readonly DrawingsAppGroupsDbContext context;

        public UserService(DrawingsAppGroupsDbContext context) 
            => this.context = context;
        private async Task ChangeRole(string userId, int groupId,Role role)
        {
            var userGroup = await context.UserGroups
                .Where(ug => ug.UserId == userId && ug.GroupId == groupId)
                .FirstOrDefaultAsync();

            userGroup.Role = role;
            context.UserGroups.Update(userGroup);
            await context.SaveChangesAsync();
        }
        public async Task CreateUser(string userId,string userName)
        {
            var user = new User
            {
                Id = userId,
                Username = userName
            };
            await context.AddAsync(user);
            await context.SaveChangesAsync();
        }
        public async Task AcceptUser(string userId, int groupId) 
            => await ChangeRole(userId, groupId, Role.User);
        public async Task PromoteUser(string userId, int groupId) 
            => await ChangeRole(userId, groupId, Role.Admin);
        public Task<bool> UserExists(string userId) 
            => context.Users.AnyAsync(u => u.Id == userId);
        public async Task<Role> JoinGroup(string userId, int groupId)
        {
            UserGroup userGroup;
            Role role;
            if (await context.Groups
                .Where(g=>g.Id==groupId)
                .Select(g=>g.GroupType)
                .FirstOrDefaultAsync()==GroupType.Public)
            {
               userGroup = new UserGroup
                {
                    UserId = userId,
                    GroupId = groupId,
                    Role=Role.User
                };
                role = Role.User;
            }
            else
            {
                userGroup = new UserGroup
                {
                    UserId = userId,
                    GroupId = groupId,
                    Role = Role.Pending
                };
                role = Role.Pending;
            }
           
            await context.UserGroups.AddAsync(userGroup);
            await context.SaveChangesAsync();
            return role;
        }

        public Task<bool> IsAdmin(string userId, int groupId) 
            => context.UserGroups
                .Where(ug => ug.UserId == userId && ug.GroupId == groupId)
                .Select(ug => ug.Role == Role.Admin)
                .FirstOrDefaultAsync();

        public async Task<Role> GetRole(string userId, int groupId) 
            => await context.UserGroups
                .Where(ug => ug.UserId == userId && ug.GroupId == groupId)
                .Select(ug => ug.Role)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<UserOutputModel>> GetUsersByGroup(int groupId,Role role) 
            => await context.UserGroups
                .Where(ug => ug.GroupId == groupId && (int)ug.Role==(int)role)
                .Select(ug => new UserOutputModel
                {
                    UserId = ug.UserId,
                    Username = ug.User.Username,
                    Role=ug.Role
                }).ToListAsync();

        public async Task<bool> LeaveGroup(string userId, int groupId)
        {
            var userGroup =await context.UserGroups
                .Where(ug => ug.UserId == userId && ug.GroupId == groupId)
                .FirstOrDefaultAsync();
            if (userGroup == null)
            {
                return false;
            }
            context.UserGroups.Remove(userGroup);
            await context.SaveChangesAsync();
            return true;
        }

        public Task<UserRoleAndGroupTypeOutputModel> GetRoleAndGroupTypeAsync(string userId, int groupId) 
            => context.Groups
                .Where(g => g.Id == groupId)
                .Select(g => new UserRoleAndGroupTypeOutputModel
                {
                    GrouptType = g.GroupType,
                    Role = g.UserGrops.Where(ug=>ug.UserId==userId).Select(ug=>ug.Role).FirstOrDefault()
                })
                .FirstOrDefaultAsync();
    }
}
