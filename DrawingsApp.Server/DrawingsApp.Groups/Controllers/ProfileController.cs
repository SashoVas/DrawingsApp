using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.Profile;
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

        [HttpGet("Full/{id?}")]
        public async Task<ActionResult> GetUserProfile(string? id)
        {
            ProfileOutputModel result;
            if (id is null)
            {
                result = await profileService.GetCallerFullProfile(GetUserId());
            }
            else
            {
                result=await profileService.GetFullProfile(id ,GetUserId());
            }
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
        [HttpGet]
        public async Task<ActionResult> GetProfile()
            => Ok(await profileService.GetProfileInfo(GetUserId()));
        [HttpPut]
        public async Task<ActionResult> Edit(EditProfileInputModel input)
        {
            if (!await profileService.EditProfile(GetUserId(),input.Description,input.ImgUrl))
            {
                return Unauthorized();
            }
            return Ok();
        }
    }
}
