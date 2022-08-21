using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Comments.Services
{
    public class GroupService: IGroupService
    {
        private readonly IGroupRepository repo;

        public GroupService(IGroupRepository repo) => this.repo = repo;

        public async Task AddUserToGroup(int groupId, string userId, Role groupRole)
        {
            var group=await repo.GetGroup(groupId);

            group.Users.Add(new UserRoleInGroup
            {
                Role = groupRole,
                UserId = userId
            });
            await repo.UpdateGroup(group);
        }

        public async Task CreateGroup(int groupId, string groupName, GroupType groupType, string userId)
        {
            var group = new GroupInfo 
            { 
                GroupId=groupId,
                GroupName=groupName,
                GroupType=groupType,
                Users=new List<UserRoleInGroup> { 
                    new UserRoleInGroup 
                    { 
                        Role=Role.Admin,
                        UserId=userId
                    }
                }
            };
            await repo.CreateGroup(group);
        }

        public Task DeleteGroup(int groupId) 
            => repo.DeleteGroup(groupId);

        public Task<GroupInfo> GetGroup(int groupId) 
            => repo.GetGroup(groupId);

        public async Task<Role> GetRole(int groupId, string userId) 
            => (await repo.GetGroup(groupId)).Users
                .Where(u => u.UserId == userId)
                .Select(u => u.Role)
                .FirstOrDefault();

        public async Task RemoveRole(int groupId, string userId)
        {
            var group = await repo.GetGroup(groupId);

            var userToRemove = group.Users.Where(u => u.UserId == userId).FirstOrDefault();
            group.Users.Remove(userToRemove);
            await repo.UpdateGroup(group);
        }

        public async Task UpdateGroup(int groupId, string groupName, GroupType groupType)
        {
            var group = await repo.GetGroup(groupId);
            group.GroupName = groupName;
            group.GroupType = groupType;
            await repo.UpdateGroup(group);
        }

        public async Task UpdateRole(int groupId, string userId, Role groupRole)
        {
            var group = await repo.GetGroup(groupId);

            group.Users
                .Where(u=>u.UserId==userId)
                .FirstOrDefault()!.Role=groupRole;
            await repo.UpdateGroup(group);
        }
    }
}
