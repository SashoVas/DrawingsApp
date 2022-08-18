namespace DrawingsApp.Messages.Post
{
    public class PostLikeMessage
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PostId { get; set; }
        public int GroupId { get; set; }
        public int ChangeAmounth { get; set; }
    }
}
