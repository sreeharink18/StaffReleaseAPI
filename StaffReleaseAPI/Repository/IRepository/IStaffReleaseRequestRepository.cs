using StaffReleaseAPI.Models;

namespace StaffReleaseAPI.Repository.IRepository
{
    public interface IStaffReleaseRequestRepository : IRepository<StaffReleaseRequest>
    {
        Task UpdateAsync(StaffReleaseRequest staffReleaseRequest);
    }
}
