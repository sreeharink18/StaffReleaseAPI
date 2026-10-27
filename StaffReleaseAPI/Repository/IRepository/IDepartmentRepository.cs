using StaffReleaseAPI.Models;

namespace StaffReleaseAPI.Repository.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task UpdateAsync(Department department);
    }
}
