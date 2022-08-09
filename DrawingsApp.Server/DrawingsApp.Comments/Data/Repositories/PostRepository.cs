using DrawingsApp.Comments.Data.Models;
using MongoDB.Driver;

namespace DrawingsApp.Comments.Data.Repositories
{
    public class PostRepository: IPostRepository
    {
        private readonly IMongoCollection<Post> collection;
        public PostRepository(MongoDbCommentsDb mongoDb,IConfiguration configuration)
        {
            collection = mongoDb.GetCollection<Post>(configuration.GetSection("MongoDbSettings:Collection").Value);
        }
        public async Task<Post> GetPost(int postId)
            => await (await collection.FindAsync(p => p.OuterId == postId)).FirstOrDefaultAsync();
        public async Task<IEnumerable<Post>> GetPosts()
            => await (await collection.FindAsync(_ => true)).ToListAsync();
        public async Task CreatePost(Post post)
            => await collection.InsertOneAsync(post);
        public async Task UpdatePost(Post post)
        {
            var filter = Builders<Post>.Filter.Eq("Id", post.Id);
            await collection.ReplaceOneAsync(filter, post);
        }
    }
}
