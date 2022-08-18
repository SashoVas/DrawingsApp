using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Comments.Models.InputModels.Post
{
    public class UpdatePostInputModel
    {
        public string PostId { get; set; }
        [MaxLength(DataConstants.PostTitleMaxLength)]
        [MinLength(DataConstants.PostTitleMinLength)]
        public string Title { get; set; }
        [MaxLength(DataConstants.PostDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
