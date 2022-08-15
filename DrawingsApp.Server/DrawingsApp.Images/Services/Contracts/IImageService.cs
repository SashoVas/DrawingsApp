using DrawingsApp.Images.Models.Output;

namespace DrawingsApp.Images.Services.Contracts
{
    public interface IImageService
    {
        Task<IEnumerable<ImageOutputModel>> GetUserImages(string userId,int page);
        Task<string> CreateImage(string userId, IFormFile inputImage,string name);
    }
}
