using StaffReleaseAPI.DataAccess.Data;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Repository.IRepository;

namespace StaffReleaseAPI.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private ApplicationDbContext _db;
        public DepartmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(Department model)
        {
            _db.Departments.Update(model);
        }
    }
}
