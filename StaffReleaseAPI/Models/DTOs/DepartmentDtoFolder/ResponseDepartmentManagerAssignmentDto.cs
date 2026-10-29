using StaffReleaseAPI.Models.DTOs.UserDtoFolder;

namespace StaffReleaseAPI.Models.DTOs.DepartmentDtoFolder
{
    public class ResponseDepartmentManagerAssignmentDto
    {
        public int AssignmentId { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
        public string ManagerUserId { get; set; } = string.Empty;

        public ManagerUserDetailDto? ManagerUser { get; set; }

        public string AssignedById { get; set; } = string.Empty;
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}
