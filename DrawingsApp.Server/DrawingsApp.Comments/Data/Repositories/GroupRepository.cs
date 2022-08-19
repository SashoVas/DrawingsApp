using DrawingsApp.Comments.Data.Models;
using MongoDB.Driver;

namespace DrawingsApp.Comments.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMongoCollection<GroupInfo> collection;
        public GroupRepository(MongoDbCommentsDb db, IConfiguration configuration)
            => collection = db.GetCollection<GroupInfo>(configuration.GetSection("MongoDbSettings:Collections:Groups").Value);
        public async Task CreateGroup(GroupInfo group) 
            => await collection.InsertOneAsync(group);

        public async Task DeleteAllGroups() 
            => await collection.DeleteManyAsync(_ => true);

        public Task<GroupInfo> GetGroup(int groupId) 
            => collection.Find(g => g.GroupId == groupId).FirstOrDefaultAsync();

        public async Task<IEnumerable<GroupInfo>> GetAllGroups() 
            => await collection.Find(_ => true).ToListAsync();

        public Task UpdateGroup(GroupInfo group) 
            => collection.ReplaceOneAsync(g => g.GroupId == group.GroupId, group);

        public Task DeleteGroup(int groupId)
        {
            return collection.DeleteOneAsync(g=>g.GroupId==groupId);
        }
    }
}
