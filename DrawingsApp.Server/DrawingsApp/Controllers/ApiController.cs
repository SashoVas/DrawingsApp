using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrawingsApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController: ControllerBase
    {
        protected string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    
}
