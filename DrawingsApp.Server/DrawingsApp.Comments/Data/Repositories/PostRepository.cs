using DrawingsApp.Comments.Data.Models;
using MongoDB.Driver;

namespace DrawingsApp.Comments.Data.Repositories
{
    public class PostRepository: IPostRepository
    {
        private readonly IMongoCollection<Post> collection;
        public PostRepository(MongoDbCommentsDb mongoDb,IConfiguration configuration)
        {
            collection = mongoDb.GetCollection<Post>(configuration.GetSection("MongoDbSettings:Collections:Comments").Value);
        }
        public Task<Post> GetPost(string postId)
        {
            return collection.Find(p => p.Id == postId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Post>> GetPosts()
            => await (await collection.FindAsync(_ => true)).ToListAsync();
        public async Task CreatePost(Post post)
            => await collection.InsertOneAsync(post);
        public async Task UpdatePost(Post post)
        {
            var filter = Builders<Post>.Filter.Eq("Id", post.Id);
            await collection.ReplaceOneAsync(filter, post);
        }
        public async Task DeletePosts()
            => await collection.DeleteManyAsync(_ => true);
    }
}
