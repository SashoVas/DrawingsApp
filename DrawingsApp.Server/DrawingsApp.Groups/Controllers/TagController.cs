using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.Tag;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    [Authorize]
    public class TagController : ApiController
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTagInfo(int id) 
            => Ok(await tagService.GetTagInfo(id));
        [HttpPost]
        public async Task<ActionResult> Create(CreateTagInputModel input) 
            => Created(nameof(GetTagInfo), await tagService.Create(input.TagName, input.TagInfo));
    }
}
