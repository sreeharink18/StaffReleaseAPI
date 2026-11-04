using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffReleaseAPI.Models
{
    public class DepartmentUserAssignment
    {
        [Key]
        public int AssignmentId { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public bool IsManager { get; set; } = false;

        public bool CanViewClearance { get; set; } = true;

        public bool CanApproveClearance { get; set; } = false;

        public bool IsActive { get; set; } = true;
        public string AssignedById { get; set; } = string.Empty;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
