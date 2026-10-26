using System.ComponentModel.DataAnnotations;

namespace StaffReleaseAPI.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

    }
}
