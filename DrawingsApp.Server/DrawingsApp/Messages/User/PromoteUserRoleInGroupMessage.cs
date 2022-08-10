using DrawingsApp.Data.Common;

namespace DrawingsApp.Messages.User
{
    public class PromoteUserRoleInGroupMessage
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }
        public Role Role { get; set; }
    }
}
