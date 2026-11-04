namespace StaffReleaseAPI.Models.DTOs.DepartmentDtoFolder
{
    public class RequestDepartmentUserAssignmentCreationDto
    {
        public int DepartmentId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public bool IsManager { get; set; } = false;

        public bool CanViewClearance { get; set; } = true;

        public bool CanApproveClearance { get; set; } = false;
        public string AssignedById { get; set; } = string.Empty;
    }
}
