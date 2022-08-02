namespace DrawingsApp.Comments.Models.InputModels.Comments
{
    public class CreateCommentOnCommentInputModel
    {
        public int PostId { get; set; }
        public List<string> CommentsPath { get; set; }
        public string Contents { get; set; }
    }
}
