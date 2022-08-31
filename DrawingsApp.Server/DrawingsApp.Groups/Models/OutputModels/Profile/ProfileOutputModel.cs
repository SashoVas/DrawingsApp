using DrawingsApp.Groups.Models.OutputModels.Group;
using DrawingsApp.Groups.Models.OutputModels.Post;

namespace DrawingsApp.Groups.Models.OutputModels.Profile
{
    public class ProfileOutputModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public IEnumerable<PostOutputModel> Posts { get; set; }
        public IEnumerable<GroupListingOutputModel> Groups { get; set; }
    }
}
