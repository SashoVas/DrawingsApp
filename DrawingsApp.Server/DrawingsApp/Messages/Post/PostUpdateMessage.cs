namespace DrawingsApp.Messages.Post
{
    public class PostUpdateMessage
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
