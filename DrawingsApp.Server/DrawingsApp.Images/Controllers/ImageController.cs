using DrawingsApp.Images.Data;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Images.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly DrawingsAppDbContext context;
        public ImageController(DrawingsAppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Index()
        {
            var ids = context.Images.ToList();
            return Ok(ids);
        }
    }
}
