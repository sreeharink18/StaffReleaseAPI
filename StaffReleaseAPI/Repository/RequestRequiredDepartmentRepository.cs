using StaffReleaseAPI.DataAccess.Data;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Repository.IRepository;

namespace StaffReleaseAPI.Repository
{
    public class RequestRequiredDepartmentRepository : Repository<RequestRequiredDepartment>, IRequestRequiredDepartmentRepository
    {
        private ApplicationDbContext _db;
        public RequestRequiredDepartmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(RequestRequiredDepartment model)
        {
            _db.RequestRequiredDepartments.Update(model);
        }
    }
}
