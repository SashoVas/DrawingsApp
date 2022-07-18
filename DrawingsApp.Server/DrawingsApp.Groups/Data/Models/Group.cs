namespace DrawingsApp.Groups.Data.Models
{
    public class Group
    {
        public Group()
        {
            GroupTags = new HashSet<GroupTag>();
            UserGrops = new HashSet<UserGroup>();
            Posts = new HashSet<Post>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string MoreInfo { get; set; }
        public ICollection<GroupTag> GroupTags { get; set; }
        public ICollection<UserGroup> UserGrops { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}