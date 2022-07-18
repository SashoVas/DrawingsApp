using DrawingsApp.Groups.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DrawingsApp.Groups.Data
{
    public class DrawingsAppGroupsDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<GroupTag> GroupTag { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DrawingsAppGroupsDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GroupTag>()
                .HasKey(gt=>new { gt.TagId,gt.GroupId})
                .HasName("PrimaryKey_GroupTagId");

            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId })
                .HasName("PrimaryKey_UserGroupId");

            modelBuilder.Entity<Group>()
                .HasMany(g => g.GroupTags)
                .WithOne(gt => gt.Group)
                .HasForeignKey(gt => gt.GroupId);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.GroupTags)
                .WithOne(gt => gt.Tag)
                .HasForeignKey(gt => gt.TagId);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Posts)
                .WithOne(p => p.Group)
                .HasForeignKey(p=>p.GroupId);

        }
    }
}
