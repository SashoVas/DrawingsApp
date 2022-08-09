using DrawingsApp.Comments.Data.Models;

namespace DrawingsApp.Comments.Models.OutputModels.Post
{
    public class PostOutputModel
    {
        public int OuterId { get; set; }
        public string PostedOn { get; set; }
        public string Title { get; set; }
        public int GroupId { get; set; }
        public int Likes { get; set; }
        public string GroupName { get; set; }
        public string SenderId { get; set; }
        public string Description { get; set; }
        public string SenderName { get; set; }
        public PostType PostType { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<string> ImgUrls { get; set; }
    }
}
