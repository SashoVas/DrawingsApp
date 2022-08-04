namespace DrawingsApp.Comments.Data.Models
{
    public interface ICommentable
    {
        ICollection<Comment> Comments { get; set; }

    }
}
