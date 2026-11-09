using StaffReleaseAPI.Models.DTOs.UserDtoFolder;

namespace StaffReleaseAPI.Models.DTOs.DepartmentDtoFolder
{
    public class ResponseDepartmentUserAssignmentDto
    {
        public int AssignmentId { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
        public string UserId { get; set; } = string.Empty;

        public UserDetailDto? User { get; set; }
        public bool IsManager { get; set; }
        public bool CanViewClearance { get; set; }
        public bool CanApproveClearance { get; set; }
        public bool IsActive { get; set; } = true;
        //public string AssignedById { get; set; } = string.Empty;
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
