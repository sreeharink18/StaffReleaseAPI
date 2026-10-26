using StaffReleaseAPI.Services.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffReleaseAPI.Models
{
    public class RequestRequiredDepartment
    {
        public int RequestRequiredDepartmentId { get; set; }

        public int RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public StaffReleaseRequest StaffReleaseRequest { get; set; } = null!;

        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; } = null!;

        // Manager assigned when the request is submitted
        public string AssignedManagerUserId { get; set; } = string.Empty;
        [ForeignKey(nameof(AssignedManagerUserId))]
        public ApplicationUser AssignedManagerUser { get; set; } = null!;

        public ClearanceStatus Status { get; set; } = ClearanceStatus.Pending;

        public string? ApprovedById { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public string? Comments { get; set; }
    }
}
