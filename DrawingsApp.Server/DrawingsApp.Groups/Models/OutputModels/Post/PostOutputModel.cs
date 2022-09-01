namespace DrawingsApp.Groups.Models.OutputModels.Post
{
    public class PostOutputModel
    {
        public string Id { get; set; }
        public List<string> ImgUrls { get; set; }
        public string PostedOn { get; set; }
        public string Title { get; set; }
        public string GroupName { get; set; }
        public string GroupImgUrl { get; set; }
        public string SenderUserName { get; set; }
        public string SenderId { get; set; }
        public int Likes { get; set; }
        public int GroupId { get; set; }

    }
}
