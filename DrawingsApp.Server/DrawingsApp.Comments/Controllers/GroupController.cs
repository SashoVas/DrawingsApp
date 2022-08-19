using DrawingsApp.Comments.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Comments.Controllers
{
    //For Debuging
    [Route("[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository repo;

        public GroupController(IGroupRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetGroups()
        {
            return Ok( await repo.GetAllGroups());
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAllGroups()
        {
            await repo.DeleteAllGroups();
            return Ok();
        }
    }
}
