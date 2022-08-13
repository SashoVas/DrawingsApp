using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Models.OutputModels.Post;

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
            ICollection<string>ImgUrls,
            PostType postType);
        Task<PostOutputModel> GetPost(int id);
        Task<IEnumerable<Post>> GetPosts();
        Task DeletePost(int outerId);
        Task UpdatePost(int outerId,string title,string description);
        Task LikePost(int outerId, int changeAmounth);
        Task DeletePosts();
    }
}
