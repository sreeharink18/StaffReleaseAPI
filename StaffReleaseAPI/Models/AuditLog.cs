namespace StaffReleaseAPI.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        public int? RequestId { get; set; }

        public string ActorUserId { get; set; } = string.Empty;

        public string ActionType { get; set; } = string.Empty;

        public string? EntityType { get; set; }
        public string? EntityId { get; set; }

        public string? OldValue { get; set; }
        public string? NewValue { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? CorrelationId { get; set; }
    }
}
