namespace DrawingsApp.Images.Services.Contracts
{
    public interface IImageService
    {
        Task<IEnumerable<object>> GetUserImages(string userId,int page);
        Task<string> CreateImage(string userId, IFormFile inputImage);
    }
}
