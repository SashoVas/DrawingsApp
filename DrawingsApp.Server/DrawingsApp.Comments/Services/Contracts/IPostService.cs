using DrawingsApp.Comments.Data.Models;

namespace DrawingsApp.Comments.Services.Contracts
{
    public interface IPostService
    {
        Task<bool> CreatePost(
            int outerId,
            int groupId,
            string groupName,
            string title,
            string explanation,
            string senderId,
            string senderName);
        Task<Post> GetPost(int id);
        Task<IEnumerable<Post>> GetPosts();
    }
}
