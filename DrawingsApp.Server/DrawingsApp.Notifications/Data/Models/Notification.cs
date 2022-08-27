using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DrawingsApp.Notifications.Data.Models
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public int GroupId { get; set; }
    }
}
