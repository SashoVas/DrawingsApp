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
            string description,
            string senderId,
            string senderName,
            ICollection<string>ImgUrls);
        Task<Post> GetPost(int id);
        Task<IEnumerable<Post>> GetPosts();
        Task DeletePost(int outerId);
        Task UpdatePost(int outerId,string title,string description);
    }
}
