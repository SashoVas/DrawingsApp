using DrawingsApp.Controllers;
using DrawingsApp.Groups.Models.InputModels.Group;
using DrawingsApp.Groups.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Groups.Controllers
{
    [Authorize]
    public class GroupController : ApiController
    {
        private readonly IGroupService groupService;
        public GroupController(IGroupService groupService) 
            => this.groupService = groupService;
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) =>
            Ok(await groupService.GetGroup(id));
        [HttpPost]
        public async Task<ActionResult> Create(CreateGroupInputModel input) 
            => Created(nameof(Get), await groupService.CreateGroup(input.Title, input.MoreInfo,input.Tags));
    }
}
