
using StaffReleaseAPI.DataAccess.Data;
using StaffReleaseAPI.Repository.IRepository;

namespace StaffReleaseAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }
        public IDepartmentRepository DepartmentRepository { get; private set; }
        public IDepartmentManagerAssignmentRepository DepartmentManagerAssignmentRepository { get; private set; }
        public IRequestRequiredDepartmentRepository RequestRequiredDepartmentRepository { get; private set; }
        public IStaffReleaseRequestRepository StaffReleaseRequestRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUserRepository = new ApplicationUserRepository(_db);
            DepartmentRepository = new DepartmentRepository(_db);
            DepartmentManagerAssignmentRepository = new DepartmentManagerAssignmentRepository(_db);
            RequestRequiredDepartmentRepository = new RequestRequiredDepartmentRepository(_db);
            StaffReleaseRequestRepository = new StaffReleaseRequestRepository(_db);
        }
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
