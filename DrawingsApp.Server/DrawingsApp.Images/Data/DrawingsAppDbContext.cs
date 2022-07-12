using DrawingsApp.Images.Models;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Images.Data
{
    public class DrawingsAppDbContext : DbContext
    {
        public DbSet<ImageFile> Images { get; set; }
        public DrawingsAppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
