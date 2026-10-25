namespace StaffReleaseAPI.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUserRepository { get; }



        Task Save();
    }
}
