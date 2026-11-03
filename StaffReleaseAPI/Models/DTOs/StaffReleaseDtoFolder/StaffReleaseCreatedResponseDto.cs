using StaffReleaseAPI.Services.Helper;

namespace StaffReleaseAPI.Models.DTOs.StaffReleaseDtoFolder
{
    public class StaffReleaseCreatedResponseDto
    {
        public int RequestId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;

        public string Status { get; set; } = StaticData.ReleaseStatus_ClearancePending;

        public DateTime CreatedAt { get; set; }

        public List<CreatedClearanceDto> Clearances { get; set; }
            = new();
    }
    public class CreatedClearanceDto
    {
        public int DepartmentId { get; set; }

        public string AssignedManagerUserId { get; set; }
            = string.Empty;

        public string Status { get; set; } = StaticData.ClearanceStatus_Pending;
    }
}
