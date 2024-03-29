﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DrawingsApp.Images.Data.Models
{
    public class ImageFile
    {
        public ImageFile()
        {
            CreatedOn = DateTime.UtcNow;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ImageFolder { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
    }
}
