using StaffReleaseAPI.Models;

namespace StaffReleaseAPI.Repository.IRepository
{
    public interface IDepartmentUserAssignmentRepository : IRepository<DepartmentUserAssignment>
    {
        Task UpdateAsync(DepartmentUserAssignment model);
    }
}
