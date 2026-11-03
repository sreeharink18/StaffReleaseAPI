namespace StaffReleaseAPI.Models.DTOs.StaffReleaseDtoFolder
{
    public class CreateStaffReleaseRequestDto
    {
        public string EmpCode { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public DateTime LastWorkingDay { get; set; }

        // Optional ERP / GOLD IDs
        public bool HasErpIds { get; set; }

        public string? CashierId { get; set; }
        public string? GoldId { get; set; }
        public string? VoidId { get; set; }
        public string? IVisionId { get; set; }

        // Request workflow
        // public ReleaseStatus Status { get; set; } = ReleaseStatus.ClearancePending;

        public string CreatedById { get; set; } = string.Empty;

        public List<int> RequiredDepartmentIds { get; set; } = new();
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
