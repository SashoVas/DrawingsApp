using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Groups.Models.InputModels.Profile
{
    public class EditProfileInputModel
    {
        [MaxLength(DataConstants.ProfileDescriptionLength)]
        public string Description { get; set; }
        public string ImgUrl { get; set; }
    }
}
