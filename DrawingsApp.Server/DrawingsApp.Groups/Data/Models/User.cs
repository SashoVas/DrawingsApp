namespace DrawingsApp.Groups.Data.Models
{
    public class User
    {
        public User()
        {
            Posts=new HashSet<Post>();
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<PostUserLikes> LikedPosts { get; set; }
    }
}
