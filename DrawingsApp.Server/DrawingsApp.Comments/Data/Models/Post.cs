using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DrawingsApp.Comments.Data.Models
{
    public class Post: ICommentable
    {
        public Post()
        {
            Comments=new List<Comment>();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime PostedOn { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }
        public int Likes { get; set; }
        public int GroupId { get; set; }
        public SenderInfo Sender { get; set; }
        public string Description { get; set; }
        public bool IsDeleated { get; set; } = false;
        public ICollection<Comment> Comments { get; set; }
        public ICollection<string> ImgUrls { get; set; }
    }
}
