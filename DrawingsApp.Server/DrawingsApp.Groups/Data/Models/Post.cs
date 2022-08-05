using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Groups.Data.Models
{
    public class Post
    {
        public Post()
        {
            Images = new HashSet<Image>();
            Likes = new HashSet<PostUserLikes>();
        }
        public int Id { get; set; }
        public DateTime PostedOn { get; set; }
        [MaxLength(DataConstants.PostTitleMaxLength)]
        [MinLength(DataConstants.PostTitleMinLength)]
        public string Title { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<PostUserLikes> Likes { get; set; }
    }
}
