using DrawingsApp.Comments.Data.Models;
using DrawingsApp.Comments.Models.OutputModels.Group;
using DrawingsApp.Comments.Models.OutputModels.User;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Comments.Models.OutputModels.Post
{
    public class PostOutputModel
    {
        public string Id { get; set; }
        public string PostedOn { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public Role Role { get; set; }
        public string Description { get; set; }
        public bool IsMe { get; set; }
        public UserOutputModel User { get; set; }
        public GroupOutputModel Group { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<string> ImgUrls { get; set; }
    }
}
