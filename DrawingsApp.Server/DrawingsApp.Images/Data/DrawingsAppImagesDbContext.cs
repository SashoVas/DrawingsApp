using DrawingsApp.Images.Models;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Images.Data
{
    public class DrawingsAppImagesDbContext : DbContext
    {
        public DbSet<ImageFile> Images { get; set; }
        public DrawingsAppImagesDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
