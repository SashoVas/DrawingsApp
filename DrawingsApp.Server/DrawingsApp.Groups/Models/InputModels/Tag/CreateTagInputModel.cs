using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Groups.Models.InputModels.Tag
{
    public class CreateTagInputModel
    {
        [MaxLength(DataConstants.TagNameMaxLength)]
        [MinLength(DataConstants.TagNameMinLength)]
        public string TagName { get; set; }
        [MaxLength(DataConstants.TagInfoMaxLenght)]
        public string TagInfo { get; set; }
    }
}
