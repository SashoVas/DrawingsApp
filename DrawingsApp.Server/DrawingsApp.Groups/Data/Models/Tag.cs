namespace DrawingsApp.Groups.Data.Models
{
    public class Tag
    {
        public Tag()
        {
            GroupTags = new HashSet<GroupTag>();
        }
        public int Id { get; set; }
        public string TagName { get; set; }
        public string TagInfo { get; set; }
        public ICollection<GroupTag> GroupTags { get; set; }

    }
}
