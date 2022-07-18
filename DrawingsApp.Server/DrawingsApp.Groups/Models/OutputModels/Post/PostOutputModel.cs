namespace DrawingsApp.Groups.Models.OutputModels.Post
{
    public class PostOutputModel
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public DateTime PostedOn { get; set; }
        public string Title { get; set; }
        public string SenderUserName { get; set; }

    }
}
