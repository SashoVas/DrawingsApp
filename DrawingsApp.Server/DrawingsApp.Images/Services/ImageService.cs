﻿using DrawingsApp.Images.Data;
using DrawingsApp.Images.Data.Models;
using DrawingsApp.Images.Models.Output;
using DrawingsApp.Images.Services.Contracts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace DrawingsApp.Images.Services
{
    public class ImageService : IImageService
    {
        private const int ThumbnailSize = 200;
        private readonly MongoDbImagesRepository repo;
        public ImageService(MongoDbImagesRepository repo) 
            => this.repo = repo;

        public async Task<string> CreateImage(string userId,IFormFile inputImage,string name)
        {
            var totalImages = await repo.Count();
            var folder =(totalImages % 1000).ToString();
            var image = new ImageFile
            {
                UserId = userId,
                ImageFolder = folder,
                Name=name
            };
            await repo.Insert(image);
            await SaveOnFileSystem(inputImage,folder,image.Id);
            return image.Id;
        }
        private async Task SaveOnFileSystem( IFormFile inputImage,string folder,string imageId)
        {
            using var imageResult = await Image.LoadAsync(inputImage.OpenReadStream());
            imageResult.Metadata.ExifProfile = null;
            var path = "wwwroot/Images/" + folder + "/";
            var storagePath = Path.Combine(Directory.GetCurrentDirectory(), path).Replace("/", "\\");
            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }
            await imageResult.SaveAsJpegAsync(storagePath+ imageId + ".jpg");
            var ratio = imageResult.Width / imageResult.Height;
            if (ratio<1)
            {
                imageResult.Mutate(i => i
                    .Resize(new Size(ThumbnailSize * ratio, ThumbnailSize)));
            }
            else
            {
                imageResult.Mutate(i => i
                    .Resize(new Size(ThumbnailSize, ThumbnailSize/ ratio)));
            }
            
            await imageResult.SaveAsJpegAsync(storagePath + imageId +".thumbnail" +".jpg");
        }
        public async Task<IEnumerable<ImageOutputModel>> GetUserImages(string userId, int page)
            => (await repo.GetByUser(userId, page))
            .Select(i => new ImageOutputModel
            {
                Id=i.Id,
                ImgUrl = i.ImageFolder + "/" + i.Id,
                Name=i.Name
            });

        public Task DeleteAll() 
            => repo.DeleteAll();

        public Task DeleteImages(string userId, IEnumerable<string> images) 
            => repo.UpdateToDeleted(userId, images);

        public async Task<IEnumerable<ImageFile>> GetAll() 
            =>await repo.GetAll();
    }
}
