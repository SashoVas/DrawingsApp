using DrawingsApp.Images.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Images.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
            => this.imageService = imageService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Index() 
            => Ok(await imageService.GetUserImages(""));
        [HttpPost]
        public async Task<ActionResult<string>> CreateImage(IFormFile image) 
            => Ok(await imageService.CreateImage("smt", image));
    }
}
