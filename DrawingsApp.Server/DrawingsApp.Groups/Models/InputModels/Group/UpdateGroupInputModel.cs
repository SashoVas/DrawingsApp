using DrawingsApp.Data;
using DrawingsApp.Groups.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Groups.Models.InputModels.Group
{
    public class UpdateGroupInputModel
    {
        public int GroupId { get; set; }
        [MaxLength(DataConstants.GroupTitleMaxLenght)]
        [MinLength(DataConstants.GroupTitleMinLenght)]
        public string Title { get; set; }
        [MaxLength(DataConstants.GroupMoreInfoMaxLength)]
        public string MoreInfo { get; set; }
        public string ImgUrl { get; set; }
        public GroupType GroupType { get; set; }
        public List<int> Tags { get; set; }
    }
}
