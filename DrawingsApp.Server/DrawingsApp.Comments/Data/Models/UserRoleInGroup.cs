using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Comments.Data.Models
{
    public class UserRoleInGroup
    {
        public string UserId { get; set; }
        public Role Role { get; set; }
    }
}
