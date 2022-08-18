using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Comments.Models.InputModels.Comments
{
    public class CreateCommentOnCommentInputModel
    {
        public string PostId { get; set; }
        public List<string> CommentsPath { get; set; }
        [MaxLength(DataConstants.CommentContentsMaxLength)]
        public string Contents { get; set; }
    }
}
