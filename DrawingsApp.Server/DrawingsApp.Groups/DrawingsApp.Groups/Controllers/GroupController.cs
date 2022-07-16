using DrawingsApp.Groups.Models.InputModels;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;
        public GroupController(IGroupService groupService) 
            => this.groupService = groupService;
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) =>
            Ok(await groupService.GetGroup(id));
        [HttpPost]
        public async Task<ActionResult> Create(CreateGroupInputModel input) 
            => Created(nameof(Get), await groupService.CreateGroup(input.Title, input.MoreInfo));
    }
}
