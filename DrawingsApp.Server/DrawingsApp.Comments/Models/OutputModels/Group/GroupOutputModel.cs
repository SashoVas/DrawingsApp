using DrawingsApp.Data.Common;

namespace DrawingsApp.Comments.Models.OutputModels.Group
{
    public class GroupOutputModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public GroupType GroupType { get; set; }
    }
}
