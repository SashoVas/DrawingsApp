namespace DrawingsApp.Groups.Models.OutputModels.Post
{
    public class PostOutputModel
    {
        public int Id { get; set; }
        public List<string> ImgUrls { get; set; }
        public DateTime PostedOn { get; set; }
        public string Title { get; set; }
        public string SenderUserName { get; set; }

    }
}
