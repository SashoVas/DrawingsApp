using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Models.OutputModels.Post;

namespace DrawingsApp.Comments.Services.Contracts
{
    public interface IPostService
    {
        Task<string> CreatePost(
            int groupId,
            string title,
            string description,
            string senderId,
            string senderName,
            ICollection<string>ImgUrls);
        Task<PostOutputModel> GetPost(string id);
        Task<IEnumerable<Post>> GetPosts();
        Task<bool> DeletePost(string postId,string userId);
        Task<bool> UpdatePost(string postId, string title,string description, string userId);
        Task LikePost(string postId, int changeAmounth);
        Task DeletePosts();
    }
}
