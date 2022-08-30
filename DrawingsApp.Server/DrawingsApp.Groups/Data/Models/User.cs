namespace DrawingsApp.Groups.Data.Models
{
    public class User
    {
        public User()
        {
            Posts=new HashSet<Post>();
            LikedPosts = new HashSet<PostUserLikes>();
            UserGrops = new HashSet<UserGroup>();
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<PostUserLikes> LikedPosts { get; set; }
        public ICollection<UserGroup> UserGrops { get; set; }

    }
}
