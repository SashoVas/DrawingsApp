using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Groups.Models.InputModels.Post
{
    public class CreatePostInputModel
    {
        [MaxLength(DataConstants.PostTitleMaxLength)]
        [MinLength(DataConstants.PostTitleMinLength)]
        public string Title { get; set; }
        [MaxLength(DataConstants.PostDescriptionMaxLength)]
        public string Description { get; set; }
        public int GroupId { get; set; }
        public List<string> ImgUrls { get; set; }
    }
}
