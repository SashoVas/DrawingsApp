using DrawingsApp.Data.Common;

namespace DrawingsApp.Groups.Models.OutputModels.User
{
    public class UserOutputModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
    }
}
