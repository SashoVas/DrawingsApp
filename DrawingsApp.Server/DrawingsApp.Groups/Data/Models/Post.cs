using DrawingsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace DrawingsApp.Groups.Data.Models
{
    public class Post
    {
        public Post()
        {
            Images = new HashSet<Image>();
        }
        public int Id { get; set; }
        public DateTime PostedOn { get; set; }
        [MaxLength(DataConstants.TitleMaxLength)]
        public string Title { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
