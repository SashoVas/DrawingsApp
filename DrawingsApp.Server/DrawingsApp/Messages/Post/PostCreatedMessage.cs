namespace DrawingsApp.Messages.Post
{
    public class PostCreatedMessage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public DateTime PostedOn { get; set; }
        public int GroupId { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
    }
}
