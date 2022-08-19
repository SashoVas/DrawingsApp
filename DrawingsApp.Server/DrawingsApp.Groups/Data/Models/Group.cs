using DrawingsApp.Data;
using DrawingsApp.Data.Common;
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
        [MaxLength(DataConstants.GroupTitleMaxLenght)]
        [MinLength(DataConstants.GroupTitleMinLenght)]
        public string Title { get; set; }
        [MaxLength(DataConstants.GroupMoreInfoMaxLength)]
        public string MoreInfo { get; set; }
        public GroupType GroupType { get; set; }
        public ICollection<GroupTag> GroupTags { get; set; }
        public ICollection<UserGroup> UserGrops { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}