namespace DrawingsApp.Groups.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public DateTime PostedOn { get; set; }
        public string Title { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
