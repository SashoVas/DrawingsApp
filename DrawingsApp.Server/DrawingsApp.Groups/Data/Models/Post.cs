namespace DrawingsApp.Groups.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public DateTime PostedOn { get; set; }
        public string Title { get; set; }
        public string SenderUserName { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
