using StaffReleaseAPI.Models;

namespace StaffReleaseAPI.Repository.IRepository
{
    public interface IRequestRequiredDepartmentRepository : IRepository<RequestRequiredDepartment>
    {
        Task UpdateAsync(RequestRequiredDepartment requestRequiredDepartment);
    }
}
