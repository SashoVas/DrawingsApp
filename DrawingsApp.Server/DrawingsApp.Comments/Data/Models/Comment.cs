namespace DrawingsApp.Comments.Data.Models
{
    public class Comment: ICommentable
    {
        public Comment()
        {
            Comments=new List<Comment>();
        }
        public string Id { get; set; }
        public string Contents { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PostId { get; set; }
        public DateTime CommentedOn { get; set; } = DateTime.UtcNow;
        public ICollection<Comment> Comments { get; set; }
    }
}
