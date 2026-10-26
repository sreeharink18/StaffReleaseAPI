using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StaffReleaseAPI.Models;

namespace StaffReleaseAPI.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentManagerAssignment> DepartmentManagerAssignments { get; set; }
        public DbSet<StaffReleaseRequest> StaffReleaseRequests { get; set; }
        public DbSet<RequestRequiredDepartment> RequestRequiredDepartments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }


    }
}
