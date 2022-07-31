using DrawingsApp.Data.Seeders;
using DrawingsApp.Groups.Data.Models;

namespace DrawingsApp.Groups.Data.Seeders
{
    public class TagSeeder : ISeeder
    {
        private readonly DrawingsAppGroupsDbContext context;
        public TagSeeder(DrawingsAppGroupsDbContext context) 
            => this.context = context;
        public void Seed()
        {
            if (!context.Tags.Any())
            {
                context.Tags.Add(new Tag
                {
                    TagName = "News",
                    TagInfo = "The latest news"
                });
                context.Tags.Add(new Tag 
                { 
                    TagName="Sports",
                    TagInfo="The group is sports oriented"
                });
                context.SaveChanges();
            }
        }
    }
}
