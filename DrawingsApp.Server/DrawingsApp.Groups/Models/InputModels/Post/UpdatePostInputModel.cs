using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Groups.Models.InputModels.Post
{
    public class UpdatePostInputModel
    {
        [MaxLength(DataConstants.PostTitleMaxLength)]
        [MinLength(DataConstants.PostTitleMinLength)]
        public string Title { get; set; }
        public int PostId { get; set; }
        [MaxLength(DataConstants.PostDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
