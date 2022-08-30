using DrawingsApp.Controllers;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    public class ProfileController : ApiController
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult> GetUserProfile(string? id) 
            => Ok(await profileService.GetProfile(id ?? GetUserId()));
    }
}
