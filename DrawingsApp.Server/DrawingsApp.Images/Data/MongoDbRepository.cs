using DrawingsApp.Images.Data.Models;
using MongoDB.Driver;

namespace DrawingsApp.Images.Data
{
    public class MongoDbRepository
    {
        private readonly IMongoCollection<ImageFile> collection;
        public MongoDbRepository(IConfiguration configuration)
        {
            var client=new MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value);
            var db = client.GetDatabase(configuration.GetSection("MongoDbSettings:DataBase").Value);
            collection = db.GetCollection<ImageFile>(configuration.GetSection("MongoDbSettings:Collection").Value);
        }
        public Task Insert(ImageFile image) 
            => collection.InsertOneAsync(image);
        public async Task<IEnumerable<ImageFile>> GetByUser(string userId) 
            =>await (await collection.FindAsync(i => i.UserId == userId)).ToListAsync();
        public Task<long> Count() 
            => collection.CountAsync(_=>true);
    }
}
