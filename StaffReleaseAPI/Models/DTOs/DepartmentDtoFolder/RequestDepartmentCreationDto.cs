using System.ComponentModel.DataAnnotations;

namespace StaffReleaseAPI.Models.DTOs.DepartmentDtoFolder
{
    public class RequestDepartmentCreationDto
    {
        [Required(ErrorMessage = "DepartmentName is required")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
