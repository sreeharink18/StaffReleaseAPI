using StaffReleaseAPI.DataAccess.Data;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Repository.IRepository;

namespace StaffReleaseAPI.Repository
{
    public class DepartmentUserAssignmentRepository : Repository<DepartmentUserAssignment>, IDepartmentUserAssignmentRepository
    {
        private readonly ApplicationDbContext _db;
        public DepartmentUserAssignmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(DepartmentUserAssignment model)
        {
            _db.DepartmentUserAssignments.Update(model);
        }
    }
}
