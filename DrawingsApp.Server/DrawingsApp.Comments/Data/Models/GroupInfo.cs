using DrawingsApp.Data.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DrawingsApp.Comments.Data.Models
{
    public class GroupInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public GroupType GroupType { get; set; }
    }
}
