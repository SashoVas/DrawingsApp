using DrawingsApp.Comments.Data.Models;
using MongoDB.Driver;

namespace DrawingsApp.Comments.Data
{
    public class MongoDbCommentsRepository
    {
        private readonly IMongoCollection<Post> collection;
        public MongoDbCommentsRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value);
            var db = client.GetDatabase(configuration.GetSection("MongoDbSettings:DataBase").Value);
            collection = db.GetCollection<Post>(configuration.GetSection("MongoDbSettings:Collection").Value);
        }
        public async Task<Post> GetPost(int postId) 
            => await (await collection.FindAsync(p => p.OuterId == postId)).FirstOrDefaultAsync();
        public async Task CreatePost(Post post) 
            => await collection.InsertOneAsync(post);
    }
}
