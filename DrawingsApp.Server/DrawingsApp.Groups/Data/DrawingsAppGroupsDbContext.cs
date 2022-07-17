using DrawingsApp.Groups.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Groups.Data
{
    public class DrawingsAppGroupsDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<GroupTag> GroupTag { get; set; }
        public DrawingsAppGroupsDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GroupTag>()
                .HasKey(gt=>new { gt.TagId,gt.GroupId})
                .HasName("PrimaryKey_GroupTagId");

            modelBuilder.Entity<Group>()
                .HasMany(g => g.GroupTags)
                .WithOne(gt => gt.Group)
                .HasForeignKey(gt => gt.GroupId);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.GroupTags)
                .WithOne(gt => gt.Tag)
                .HasForeignKey(gt => gt.TagId);

        }
    }
}
