namespace DrawingsApp.Messages.Post
{
    public class PostCreatedMessage
    {
        public int Id { get; set; }
        public DateTime PostedOn { get; set; }
        public string Title { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int PostType { get; set; }
        public string Description { get; set; }
        public ICollection<string> Images { get; set; }
    }
}
