using DrawingsApp.Controllers;
using DrawingsApp.Images.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrawingsApp.Images.Controllers
{
    [Authorize]
    public class ImageController : ApiController
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
            => this.imageService = imageService;

        [HttpGet("{id?}")]
        public async Task<ActionResult<IEnumerable<object>>> GetUserImages(string? id,[FromQuery]int page=0) 
            => Ok(await imageService.GetUserImages(id??User.FindFirstValue(ClaimTypes.NameIdentifier),page));

        [HttpPost]
        public async Task<ActionResult<string>> CreateImage(IFormFile image) 
            => Created("",await imageService.CreateImage(User.FindFirstValue(ClaimTypes.NameIdentifier), image));
    }
}
