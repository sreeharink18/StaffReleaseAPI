namespace StaffReleaseAPI.Models.DTOs.DepartmentDtoFolder
{
    public class RequestDepartmentManagerAssignmentDto
    {
        public int DepartmentId { get; set; }
        public string ManagerUserId { get; set; } = string.Empty;

        public string AssignedById { get; set; } = string.Empty;
    }
}
