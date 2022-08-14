using DrawingsApp.Images.Data.Models;
using MongoDB.Driver;

namespace DrawingsApp.Images.Data
{
    public class MongoDbImagesRepository
    {
        private readonly IMongoCollection<ImageFile> collection;
        public MongoDbImagesRepository(IConfiguration configuration)
        {
            var client=new MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value);
            var db = client.GetDatabase(configuration.GetSection("MongoDbSettings:DataBase").Value);
            collection = db.GetCollection<ImageFile>(configuration.GetSection("MongoDbSettings:Collection").Value);
        }
        public Task Insert(ImageFile image) 
            => collection.InsertOneAsync(image);
        public async Task<IEnumerable<ImageFile>> GetByUser(string userId,int page) 
            =>await collection
            .Find(i => i.UserId == userId)
            .Skip(20*page)
            .Limit(20)
            .ToListAsync();
        public Task<long> Count() 
            => collection.CountAsync(_=>true);
    }
}
