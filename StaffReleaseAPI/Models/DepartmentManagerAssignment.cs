using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffReleaseAPI.Models
{
    public class DepartmentManagerAssignment
    {
        [Key]
        public int AssignmentId { get; set; }

        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        public string ManagerUserId { get; set; } = string.Empty;
        [ForeignKey("ManagerUserId")]
        public ApplicationUser? ManagerUser { get; set; }

        public string AssignedById { get; set; } = string.Empty;
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}
