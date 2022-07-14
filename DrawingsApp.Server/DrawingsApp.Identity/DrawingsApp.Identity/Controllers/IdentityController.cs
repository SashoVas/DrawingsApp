using DrawingsApp.Identity.Models.InputModels;
using DrawingsApp.Identity.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identityServer;
        private readonly IConfiguration configuration;
        public IdentityController(IIdentityService identityServer, IConfiguration configuration)
        {
            this.identityServer = identityServer;
            this.configuration = configuration;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<object>> Login(LoginInputModel input)
        {
            var secret = configuration.GetSection("AppSettings:Secret").Value;
            var token= await identityServer.Login(input.UserName, input.Password, secret);
            return Ok(new {Token=token });
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterInputModel input)
        {
            var id =await identityServer.Register(input.UserName,input.Password,input.ConfirmPassword);
            return Ok();
        }
    }
}