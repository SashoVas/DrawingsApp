namespace DrawingsApp.Groups.Data.Models
{
    public class Image
    {
        public string Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
