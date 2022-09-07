using DrawingsApp.Groups.Models.OutputModels.Profile;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IProfileService
    {
        Task<ProfileOutputModel> GetFullProfile(string userId,string callerId);
        Task<ProfileOutputModel> GetCallerFullProfile(string userId);
        Task<ProfileInfoOutputModel> GetProfileInfo(string userId);
        Task<bool> EditProfile(string userId,string description,string imgUrl);
    }
}
