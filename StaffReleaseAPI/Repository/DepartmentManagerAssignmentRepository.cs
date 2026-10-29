using StaffReleaseAPI.DataAccess.Data;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Repository.IRepository;

namespace StaffReleaseAPI.Repository
{
    public class DepartmentManagerAssignmentRepository : Repository<DepartmentManagerAssignment>, IDepartmentManagerAssignmentRepository
    {

        private ApplicationDbContext _db;
        public DepartmentManagerAssignmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(DepartmentManagerAssignment model)
        {
            //var departmentMangerAssignment = _db.DepartmentManagerAssignments.FirstOrDefault(d => d.AssignmentId == model.AssignmentId);
            //if (departmentMangerAssignment != null)
            //{
            //    if (!string.IsNullOrEmpty(departmentMangerAssignment.ManagerUserId) || departmentMangerAssignment.ManagerUserId != model.ManagerUserId)
            //    {
            //        departmentMangerAssignment.ManagerUserId = model.ManagerUserId;
            //    }
            //    if (!string.IsNullOrEmpty(departmentMangerAssignment.AssignedById) || departmentMangerAssignment.AssignedById != model.AssignedById)
            //    {
            //        departmentMangerAssignment.AssignedById = model.AssignedById;
            //    }
            //    if (departmentMangerAssignment.AssignedAt != model.AssignedAt)
            //    {
            //        departmentMangerAssignment.AssignedAt = model.AssignedAt;
            //    }
            //    if (departmentMangerAssignment.IsActive != model.IsActive)
            //    {
            //        departmentMangerAssignment.IsActive = model.IsActive;
            //    }
            //}

            _db.DepartmentManagerAssignments.Update(model);
        }
    }
}
