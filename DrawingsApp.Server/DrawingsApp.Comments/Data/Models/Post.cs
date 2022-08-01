using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DrawingsApp.Comments.Data.Models
{
    public class Post
    {
        public Post()
        {
            Comments=new List<Comment>();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int OuterId { get; set; }
        public DateTime PostedOn { get; set; }
        public string Title { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string SenderId { get; set; }
        public string Explanation { get; set; }
        public string SenderName { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
