using DrawingsApp.Groups.Models.OutputModels.Post;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IPostService
    {
        Task<List<PostOutputModel>> GetPosts(int groupId);
        Task<List<PostOutputModel>> GetPostsByUser(string userId);
        Task<int> CreatePost(string senderId, string title, int groupId, List<string> imgUrls, string outerId);
        Task<bool> UpdatePost(string outerId, string title);
        Task<bool> DeletePost(string postId);
        Task<int> LikePost( string userId,string postId,bool isLike);
    }
}
