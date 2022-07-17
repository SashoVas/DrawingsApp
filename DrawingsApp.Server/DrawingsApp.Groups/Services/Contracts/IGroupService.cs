using DrawingsApp.Groups.Models.InputModels;
using DrawingsApp.Groups.Models.OutputModels;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IGroupService
    {
        Task<int> CreateGroup(string title, string moreInfo,List<int>tags);
        Task<GroupOutputModel> GetGroup(int id);
    }
}
