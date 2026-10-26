using StaffReleaseAPI.Services.Helper;
using System.ComponentModel.DataAnnotations;

namespace StaffReleaseAPI.Models
{
    public class StaffReleaseRequest
    {
        [Key]
        public int RequestId { get; set; }

        // Employee details
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
        public ReleaseStatus Status { get; set; } = ReleaseStatus.ClearancePending;

        public string CreatedById { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? CancelledById { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }


    }
}
