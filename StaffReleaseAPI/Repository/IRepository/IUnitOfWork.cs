namespace StaffReleaseAPI.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUserRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IDepartmentManagerAssignmentRepository DepartmentManagerAssignmentRepository { get; }
        IRequestRequiredDepartmentRepository RequestRequiredDepartmentRepository { get; }
        IStaffReleaseRequestRepository StaffReleaseRequestRepository { get; }



        Task Save();
    }
}
