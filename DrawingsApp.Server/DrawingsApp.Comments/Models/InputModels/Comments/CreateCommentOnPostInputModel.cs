namespace DrawingsApp.Comments.Models.InputModels.Comments
{
    public class CreateCommentOnPostInputModel
    {
        public int PostId { get; set; }
        public string Contents { get; set; }
    }
}
