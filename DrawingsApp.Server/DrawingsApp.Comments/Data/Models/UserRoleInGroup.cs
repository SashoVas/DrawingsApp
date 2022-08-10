using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DrawingsApp.Data.Common;

namespace DrawingsApp.Comments.Data.Models
{
    public class UserRoleInGroup
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string UserId { get; set; }
        public int GroupId { get; set; }
        public Role Role { get; set; }
    }
}
