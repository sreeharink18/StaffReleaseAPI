namespace StaffReleaseAPI.Models.DTOs.DepartmentDtoFolder
{
    public class RequestDepartmentUpdationDto
    {
        public string? DepartmentName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
