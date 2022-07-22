using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Models.OutputModels.Post;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IPostService
    {
        Task<List<PostOutputModel>> GetPosts(int groupId,Role role);
        Task<List<PostOutputModel>> GetPostsByUser(string userId);
    }
}
