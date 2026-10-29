namespace StaffReleaseAPI.Models.DTOs.DepartmentDtoFolder
{
    public class RequestDepartmentManagerAssignmentUpdateDto
    {
        public int DepartmentId { get; set; }

        public string? ManagerUserId { get; set; } = string.Empty;

        public string? AssignedById { get; set; } = string.Empty;
        public DateTime? AssignedAt { get; set; } = DateTime.UtcNow;

        public bool? IsActive { get; set; } = true;
    }
}
