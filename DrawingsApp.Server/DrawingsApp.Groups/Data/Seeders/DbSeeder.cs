using DrawingsApp.Data.Seeders;

namespace DrawingsApp.Groups.Data.Seeders
{
    public class DbSeeder : ISeeder
    {
        private readonly DrawingsAppGroupsDbContext context;
        public DbSeeder(DrawingsAppGroupsDbContext context) 
            => this.context = context;
        public void Seed()
        {
            var seeders = new List<ISeeder> { new TagSeeder(context),new GroupSeeder(context) };
            seeders.ForEach(s => s.Seed());
        }
    }
}
