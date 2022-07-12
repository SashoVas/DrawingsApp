using DrawingsApp.Images.Data;
using DrawingsApp.Images.Models;
using DrawingsApp.Images.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace DrawingsApp.Images.Services
{
    public class ImageService : IImageService
    {
        private const int ImgWidth = 700;
        private const int ImgHeight = 700;
        private readonly DrawingsAppDbContext context;
        public ImageService(DrawingsAppDbContext context) 
            => this.context = context;
        public async Task<string> CreateImage(string userId,IFormFile inputImage)
        {
            var totalImages = await context.Images.CountAsync();
            var folder =(totalImages % 1000).ToString();
            var image = new ImageFile
            {
                UserId = userId,
                ImageFolder = folder,
                Expleanatoin="smt"
            };
            await SaveOnFileSystem(inputImage,folder,image.Id);
            await context.AddAsync(image);
            await context.SaveChangesAsync();
            return image.Id.ToString();
        }
        private async Task SaveOnFileSystem( IFormFile inputImage,string folder,Guid imageId)
        {
            using var imageResult = await Image.LoadAsync(inputImage.OpenReadStream());
            imageResult.Mutate(i => i
                .Resize(new Size(ImgWidth, ImgHeight)));
            imageResult.Metadata.ExifProfile = null;
            var path = "wwwroot/Images/" + folder + "/";
            var storagePath = Path.Combine(Directory.GetCurrentDirectory(), path).Replace("/", "\\");
            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }
            await imageResult.SaveAsJpegAsync(storagePath+ imageId + ".jpg");
        }
        public async Task<IEnumerable<string>> GetUserImages(string userId) 
            => await context.Images
                .Where(i => i.UserId == userId)
                .Select(i => i.ImageFolder + "/" + i.Id)
                .ToListAsync();
    }
}
