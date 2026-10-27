using StaffReleaseAPI.DataAccess.Data;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Repository.IRepository;

namespace StaffReleaseAPI.Repository
{
    public class StaffReleaseRequestRepository : Repository<StaffReleaseRequest>, IStaffReleaseRequestRepository
    {
        private ApplicationDbContext _db;
        public StaffReleaseRequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(StaffReleaseRequest model)
        {
            _db.StaffReleaseRequests.Update(model);
        }
    }
}
