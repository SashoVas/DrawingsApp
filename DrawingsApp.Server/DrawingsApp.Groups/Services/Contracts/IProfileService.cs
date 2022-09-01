using DrawingsApp.Groups.Models.OutputModels.Profile;

namespace DrawingsApp.Groups.Services.Contracts
{
    public interface IProfileService
    {
        Task<ProfileOutputModel> GetFullProfile(string userId);
        Task<ProfileInfoOutputModel> GetProfileInfo(string userId);
    }
}
