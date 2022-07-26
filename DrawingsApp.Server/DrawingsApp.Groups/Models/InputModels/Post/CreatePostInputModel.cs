namespace DrawingsApp.Groups.Models.InputModels.Post
{
    public class CreatePostInputModel
    {
        public string Title { get; set; }
        public int GroupId { get; set; }
        public List<string> ImgUrls { get; set; }
    }
}
