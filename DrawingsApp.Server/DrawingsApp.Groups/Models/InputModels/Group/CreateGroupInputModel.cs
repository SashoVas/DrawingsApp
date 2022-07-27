using DrawingsApp.Groups.Data.Models;

namespace DrawingsApp.Groups.Models.InputModels.Group
{
    public class CreateGroupInputModel
    {
        public string Title { get; set; }
        public string MoreInfo { get; set; }
        public string ImgUrl { get; set; }
        public GroupType GroupType { get; set; }
        public List<int> Tags { get; set; }
    }
}
