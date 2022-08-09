using DrawingsApp.Comments.Data.Models;
using MongoDB.Driver;

namespace DrawingsApp.Comments.Data
{
    public class MongoDbCommentsDb
    {
        private readonly IMongoDatabase db;
        public MongoDbCommentsDb(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value);
            db = client.GetDatabase(configuration.GetSection("MongoDbSettings:DataBase").Value);
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return db.GetCollection<T>(collectionName);
        }
    }
}
