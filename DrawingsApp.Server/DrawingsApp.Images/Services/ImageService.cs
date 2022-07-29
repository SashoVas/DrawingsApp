using DrawingsApp.Images.Data;
using DrawingsApp.Images.Data.Models;
using DrawingsApp.Images.Services.Contracts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace DrawingsApp.Images.Services
{
    public class ImageService : IImageService
    {
        private const int ImgWidth = 700;
        private const int ImgHeight = 700;
        private readonly MongoDbRepository repo;
        public ImageService( MongoDbRepository repo)
        {
            this.repo = repo;
        }

        public async Task<string> CreateImage(string userId,IFormFile inputImage)
        {
            var totalImages = await repo.Count();
            var folder =(totalImages % 1000).ToString();
            var image = new ImageFile
            {
                UserId = userId,
                ImageFolder = folder
            };
            await repo.Insert(image);
            await SaveOnFileSystem(inputImage,folder,image.Id);
            return image.Id;
        }
        private async Task SaveOnFileSystem( IFormFile inputImage,string folder,string imageId)
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
        public async Task<IEnumerable<object>> GetUserImages(string userId)
            => (await repo.GetByUser(userId)).Select(i => i.ImageFolder + "/" + i.Id).ToList();
            //await context.Images
               // .Where(i => i.UserId == userId)
               // .Select(i => i.ImageFolder + "/" + i.Id)
               // .ToListAsync();
    }
}
