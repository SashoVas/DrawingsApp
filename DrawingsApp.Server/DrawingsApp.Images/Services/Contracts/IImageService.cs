using DrawingsApp.Images.Data.Models;
using DrawingsApp.Images.Models.Output;

namespace DrawingsApp.Images.Services.Contracts
{
    public interface IImageService
    {
        Task<IEnumerable<ImageOutputModel>> GetUserImages(string userId,int page);
        Task<string> CreateImage(string userId, IFormFile inputImage,string name);
        Task DeleteAll();
        Task DeleteImages(string userID,IEnumerable<string>images);
        Task<IEnumerable<ImageFile>> GetAll();
    }
}
