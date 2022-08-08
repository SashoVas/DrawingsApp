using DrawingsApp.Groups.Data.Models;
using DrawingsApp.Groups.Models.InputModels.Group;
using DrawingsApp.Groups.Models.OutputModels.Group;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IGroupService
    {
        Task<int> CreateGroup(string title, string moreInfo,string ImgUrl, GroupType groupType, List<int>tags);
        Task<GroupOutputModel> GetGroup(int id);
        Task<GroupType> GetGroupType(int id);
        Task<bool> UpdateGroup(int groupId,string title,string moreInfo,string imgUrl,GroupType groupType ,List<int>tags);
        Task<bool> DeleteGroup(int groupId);
        Task<IEnumerable<GroupListingOutputModel>> Search(string? name, List<int>? tags, string? userId, GroupType? groupType, SortType orderType);
        Task<IEnumerable<GroupListingOutputModel>> GetGropusByUser(string userId);
        Task<string> GetGroupName(int groupId);
        Task<IEnumerable<GroupListingOutputModel>> GetTopGroups();
    }
}
