using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Data.Common;
using MongoDB.Driver;

namespace DrawingsApp.Comments.Data.Repositories
{
    public class UserRoleInGroupRepository : IUserRoleInGroupRepository
    {
        private readonly IMongoCollection<UserRoleInGroup> collection;
        public UserRoleInGroupRepository(MongoDbCommentsDb db, IConfiguration configuration) 
            => collection = db.GetCollection<UserRoleInGroup>(configuration.GetSection("MongoDbSettings:Collections:UserRoleInGroup").Value);
        public async Task<Role> GetRole(string userId, int groupId) 
            => (await collection
            .Find(ug => ug.UserId == userId && ug.GroupId == groupId)
            .FirstOrDefaultAsync()).Role;

        public async Task<IEnumerable<UserRoleInGroup>> GetUserRoles() 
            => await collection.Find(_ => true).ToListAsync();

        public async Task RemoveAll() => await collection.DeleteManyAsync(_ => true);

        public Task RemoveOne(string userId, int groupId) 
            => collection.DeleteOneAsync(ug => ug.UserId == userId && ug.GroupId == groupId);

        public async Task UpdateRole(UserRoleInGroup role) 
            => await collection
                       .ReplaceOneAsync(ug => ug.UserId == role.UserId && ug.GroupId == role.GroupId, role
                           , new UpdateOptions
                           {
                               IsUpsert = true
                           });
    }
}
