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
            _db.DepartmentManagerAssignments.Update(model);
        }
    }
