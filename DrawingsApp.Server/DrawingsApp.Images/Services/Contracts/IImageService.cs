namespace DrawingsApp.Images.Services.Contracts
{
    public interface IImageService
    {
        Task<IEnumerable<object>> GetUserImages(string userId);
        Task<string> CreateImage(string userId, IFormFile inputImage);
    }
}
