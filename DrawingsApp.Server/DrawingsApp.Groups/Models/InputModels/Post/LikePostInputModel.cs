namespace DrawingsApp.Groups.Models.InputModels.Post
{
    public class LikePostInputModel
    {
        public int GroupId { get; set; }
        public string PostId { get; set; }
        public bool IsLike { get; set; }
    }
}
