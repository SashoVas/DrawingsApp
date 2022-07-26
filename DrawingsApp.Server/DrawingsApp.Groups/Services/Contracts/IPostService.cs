using DrawingsApp.Groups.Models.OutputModels.Post;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IPostService
    {
        Task<List<PostOutputModel>> GetPosts(int groupId);
        Task<List<PostOutputModel>> GetPostsByUser(string userId);
        Task<int> CreatePost(string senderId, string title, int groupId, List<string> imgUrls);
        Task<bool> UpdatePost(string senderId,int postId, string title);
        Task<bool> DeletePost(string userId, int postId);
    }
}
