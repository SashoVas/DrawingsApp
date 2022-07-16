using DrawingsApp.Identity.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Identity.Data
{
    public class DrawingsAppIdentityDbContext : IdentityDbContext<User>
    {
        public DrawingsAppIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
