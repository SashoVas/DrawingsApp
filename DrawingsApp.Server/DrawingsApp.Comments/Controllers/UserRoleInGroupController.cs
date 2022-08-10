using DrawingsApp.Comments.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsApp.Comments.Controllers
{
    //For debuging
    [ApiController]
    [Route("[controller]")]
    public class UserRoleInGroupController : ControllerBase
    {
        private readonly IUserRoleInGroupRepository repo;

        public UserRoleInGroupController(IUserRoleInGroupRepository repo) 
            => this.repo = repo;

        [HttpGet]
        public async Task<ActionResult> GetUserRoles() 
            => Ok(await repo.GetUserRoles());
        [HttpDelete]
        public async Task<ActionResult> DeleteAll()
        {
            await repo.RemoveAll();
            return Ok();
        }
    }
}
