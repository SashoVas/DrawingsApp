﻿namespace DrawingsApp.Groups.Data.Models
{
    public class PostUserLikes
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
