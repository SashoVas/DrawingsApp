using DrawingsApp.Groups.Data.Models;

namespace DrawingsApp.Groups.Models.InputModels.Group
{
    public class SearchGroupInputModel
    {
        public string? Name { get; set; }
        public List<int>? Tags { get; set; }
        public string? UserId { get; set; }
        public SortType Order { get; set; }
        public GroupType? GroupType { get; set; }
    }
}
