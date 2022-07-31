using DrawingsApp.Data.Seeders;
using DrawingsApp.Groups.Data.Models;

namespace DrawingsApp.Groups.Data.Seeders
{
    public class GroupSeeder : ISeeder
    {
        private readonly DrawingsAppGroupsDbContext context;
        public GroupSeeder(DrawingsAppGroupsDbContext context) 
            => this.context = context;
        public void Seed()
        {
            if (!context.Groups.Any())
            {
                context.Groups.Add(new Group
                {
                    Title="DrawingsAppGroup",
                    GroupType=GroupType.Public,
                    MoreInfo="Latest news about the app",
                    GroupTags=new List<GroupTag> { new GroupTag {TagId=1 } }
                });
                context.SaveChanges();
            }
        }
    }
}
