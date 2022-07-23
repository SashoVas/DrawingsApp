using DrawingsApp.Groups.Models.OutputModels.Post;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IPostService
    {
        Task<List<PostOutputModel>> GetPosts(int groupId);
        Task<List<PostOutputModel>> GetPostsByUser(string userId);
        Task<int> CreatePost(string SenderId, string Title, int GroupId, string ImgUrl);
    }
}
