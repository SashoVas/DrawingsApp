using DrawingsApp.Data.Common;

namespace DrawingsApp.Messages.User
{
    public class CreateUserRoleMessage
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }
        public Role Role { get; set; }
    }
}
