namespace DrawingsApp.Groups.Models.InputModels.Group
{
    public class CreateGroupInputModel
    {
        public string Title { get; set; }
        public string MoreInfo { get; set; }
        public List<int> Tags { get; set; }
    }
}
