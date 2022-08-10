using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Groups.Models.OutputModels.Group
{
    public class GroupOutputModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MoreInfo { get; set; }
        public string ImgUrl { get; set; }
        public int Users { get; set; }
        public GroupType groupType { get; set; }
        public List<string> Tags { get; set; }
        public Role Role { get; set; }
    }
}
