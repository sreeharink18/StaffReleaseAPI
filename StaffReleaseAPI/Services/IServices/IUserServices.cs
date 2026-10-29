using StaffReleaseAPI.Services.Helper;

namespace StaffReleaseAPI.Services.IServices
{
    public interface IUserServices
    {
        Task<ApiResponse> GetUsersDetails();
    }
}
