namespace StaffReleaseAPI.Models.DTOs.StaffReleaseDtoFolder
{
    public class ResponsePendingClearanceDto
    {
        public int RequestRequiredDepartmentId { get; set; }
        public int RequestId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public DateTime LastWorkingDay { get; set; }
        public string Status { get; set; }
    }
}
