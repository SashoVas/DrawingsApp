using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Models.OutputModels.User;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Groups.Services
{
    public class UserService : IUserService
    {
        private readonly DrawingsAppGroupsDbContext context;

        public UserService(DrawingsAppGroupsDbContext context) 
            => this.context = context;
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
        public async Task AcceptUser(string userId,int groupId)
        {
            var userGroup =await context.UserGroups
                .Where(ug => ug.UserId == userId && ug.GroupId == groupId)
                .FirstOrDefaultAsync();

            userGroup.Role = Role.User;
            context.UserGroups.Update(userGroup);
            await context.SaveChangesAsync();
        }
        public async Task PromoteUser(string userId, int groupId)
        {
            var userGroup = await context.UserGroups
                .Where(ug => ug.UserId == userId && ug.GroupId == groupId)
                .FirstOrDefaultAsync();

            userGroup.Role = Role.Admin;
            context.UserGroups.Update(userGroup);
            await context.SaveChangesAsync();
        }
        public Task<bool> UserExists(string userId) 
            => context.Users.AnyAsync(u => u.Id == userId);
        public async Task JoinGroup(string userId, int groupId)
        {
            UserGroup userGroup;
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
            }
            else
            {
                userGroup = new UserGroup
                {
                    UserId = userId,
                    GroupId = groupId,
                    Role = Role.Pending
                };
            }
           
            await context.UserGroups.AddAsync(userGroup);
            await context.SaveChangesAsync();
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

        public async Task<IEnumerable<UserOutputModel>> GetUsersByGroup(int groupId) 
            => await context.UserGroups
                .Where(ug => ug.GroupId == groupId)
                .Select(ug => new UserOutputModel
                {
                    UserId = ug.UserId,
                    Username = ug.User.Username
                }).ToListAsync();
    }
}
