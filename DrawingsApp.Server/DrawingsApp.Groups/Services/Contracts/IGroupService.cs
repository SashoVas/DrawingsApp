using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Models.OutputModels.Group;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IGroupService
    {
        Task<int> CreateGroup(string title, string moreInfo, GroupType groupType, List<int>tags);
        Task<GroupOutputModel> GetGroup(int id);
    }
}
