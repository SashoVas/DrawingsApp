using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingsApp.Messages.Post
{
    public class PostCreatedMessage
    {
        public int Id { get; set; }
        public DateTime PostedOn { get; set; }
        public string Title { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Explanation { get; set; }
        public ICollection<string> Images { get; set; }
    }
}
