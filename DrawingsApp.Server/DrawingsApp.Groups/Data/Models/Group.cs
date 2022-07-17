namespace DrawingsApp.Groups.Data.Models
{
    public class Group
    {
        public Group()
        {
            GroupTags = new HashSet<GroupTag>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string MoreInfo { get; set; }
        public ICollection<GroupTag> GroupTags { get; set; }
    }
}
