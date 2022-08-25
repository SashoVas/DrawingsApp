using DrawingsApp.Controllers;
using DrawingsApp.Identity.Models.InputModels;
using DrawingsApp.Identity.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Identity.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
            try
            {
                var token= await identityServer.Login(input.UserName, input.Password, secret);
                return Ok(new {Token=token });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterInputModel input)
        {
            try
            {
                var id =await identityServer.Register(input.UserName,input.Password,input.ConfirmPassword);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}