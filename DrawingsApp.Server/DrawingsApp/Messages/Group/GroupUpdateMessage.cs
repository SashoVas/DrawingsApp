using DrawingsApp.Data.Common;

namespace DrawingsApp.Messages.Group
{
    public class GroupUpdateMessage
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string UserId { get; set; }
        public GroupType GroupType { get; set; }
    }
}
