using DrawingsApp.Controllers;
using DrawingsApp.Images.Models.Input;
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
        public async Task<ActionResult<string>> CreateImage([FromForm]CreateImageInputModel input) 
            => Created("",await imageService.CreateImage(User.FindFirstValue(ClaimTypes.NameIdentifier), input.Image,input.Name));
        [HttpDelete]
        public async Task<ActionResult> DeleteImages([FromQuery]IEnumerable<string>images)
        {
            await imageService.DeleteImages(GetUserId(),images);
            return Ok();
        }
        //For Debuging
        [HttpGet("All")]
        public async Task<ActionResult> GetImages()
        {
            return Ok(await imageService.GetAll());
        }
        [HttpDelete("All")]
        public async Task<ActionResult> DeleteAll()
        {
            await imageService.DeleteAll();
            return Ok();
        }
    }
}
