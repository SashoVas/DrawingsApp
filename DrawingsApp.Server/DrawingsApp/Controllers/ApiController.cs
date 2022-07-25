using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrawingsApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ApiController: ControllerBase
    {
        protected string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    
}
