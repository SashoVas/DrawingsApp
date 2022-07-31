using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

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
        public string? ImgUrl { get; set; }
        [MaxLength(DataConstants.TitleMaxLength)]
        public string Title { get; set; }
        [MaxLength(DataConstants.MoreInfoMaxLength)]
        public string MoreInfo { get; set; }
        public GroupType GroupType { get; set; }
        public ICollection<GroupTag> GroupTags { get; set; }
        public ICollection<UserGroup> UserGrops { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}