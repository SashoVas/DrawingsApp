using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Groups.Data.Models
{
    public class Tag
    {
        public Tag()
        {
            GroupTags = new HashSet<GroupTag>();
        }
        public int Id { get; set; }
        [MaxLength(DataConstants.TagNameMaxLength)]
        [MinLength(DataConstants.TagNameMinLength)]
        public string TagName { get; set; }
        [MaxLength(DataConstants.TagInfoMaxLenght)]
        public string TagInfo { get; set; }
        public ICollection<GroupTag> GroupTags { get; set; }

    }
}
