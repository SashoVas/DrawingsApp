using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.Tag;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    public class TagController : ApiController
    {
        private readonly ITagService tagService;
        private readonly IUserService userService;
        public TagController(ITagService tagService, IUserService userService)
        {
            this.tagService = tagService;
            this.userService = userService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTagInfo(int id) 
            => Ok(await tagService.GetTagInfo(id));
        [HttpGet]
        public async Task<ActionResult> GetTags(string? name) 
            => Ok(await tagService.GetTags(name));
        [HttpPost]
        public async Task<ActionResult> Create(CreateTagInputModel input) 
            => CreatedAtAction(nameof(GetTagInfo), await tagService.Create(input.TagName, input.TagInfo));
        [HttpPut]
        public async Task<ActionResult>SetTag(SetTagInputModel input)
        {
            if (!await userService.IsAdmin(GetUserId(), input.GroupId))
            {
                return Unauthorized();
            }
            await tagService.SetTagToGroup(GetUserId(), input.GroupId,input.TagId);
            return Ok();
        }
    }
}
