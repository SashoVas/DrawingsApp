using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.OutputModels.Group;
using DrawingsApp.Groups.Models.OutputModels.Post;
using DrawingsApp.Groups.Models.OutputModels.Profile;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    public class ProfileController : ApiController
    {
        private readonly IProfileService profileService;
        private readonly IUserService userService;

        public ProfileController(IProfileService profileService, IUserService userService)
        {
            this.profileService = profileService;
            this.userService = userService;
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult> GetUserProfile(string? id)
        {
            var result = await profileService.GetProfile(id ?? GetUserId());
            if (result is null && id is null)
            {
                await this.userService.CreateUser(GetUserId(),User.Identity.Name);
                result = new ProfileOutputModel 
                {
                    UserId=GetUserId(),
                    UserName=User.Identity.Name,
                    Groups=Enumerable.Empty<GroupListingOutputModel>(),
                    Posts=Enumerable.Empty<PostOutputModel>()
                };
            }
            return Ok(result);
        }
    }
}
