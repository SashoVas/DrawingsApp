using DrawingsApp.Notifications.Data.Models;
using MongoDB.Driver;

namespace DrawingsApp.Notifications.Data
{
    public class MongoDbNotificationsRepository
    {
        private readonly IMongoCollection<Notification> collection;
        public MongoDbNotificationsRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value);
            var db = client.GetDatabase(configuration.GetSection("MongoDbSettings:DataBase").Value);
            this.collection=db.GetCollection<Notification>(configuration.GetSection("MongoDbSettings:Collections:Notifications").Value);
        }
        public Task<Notification> Find(string userId,int groupId)
        {
            return collection
                .Find(n => n.UserId == userId && n.GroupId == groupId)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Notification>> GetUsersNotifications(string userId)
        {
            return await collection
                .Find(n => n.UserId == userId)
                .ToListAsync();
        }
        public async Task Insert(Notification notification)
        {
            await collection.InsertOneAsync(notification);
        }
        public async Task Delete(string userId, int groupId)
        {
            await collection.DeleteOneAsync(n => n.UserId == userId && n.GroupId == groupId);
        }
        public async Task DeleteAll()
        {
            await collection.DeleteManyAsync(_=>true);
        }
    }
}
