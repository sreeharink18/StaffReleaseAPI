using StaffReleaseAPI.Models;

namespace StaffReleaseAPI.Repository.IRepository
{
    public interface IDepartmentManagerAssignmentRepository : IRepository<DepartmentManagerAssignment>
    {
        Task UpdateAsync(DepartmentManagerAssignment departmentManagerAssignment);
    }
}
