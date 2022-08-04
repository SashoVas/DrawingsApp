using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Comments.Models.InputModels.Comments
{
    public class CreateCommentOnPostInputModel
    {
        public int PostId { get; set; }
        [MaxLength(DataConstants.CommentContentsMaxLength)]
        public string Contents { get; set; }
    }
}
