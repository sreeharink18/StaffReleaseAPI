using StaffReleaseAPI.Models;

namespace StaffReleaseAPI.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task UpdateAsync(ApplicationUser applicationUser);
    }
}
