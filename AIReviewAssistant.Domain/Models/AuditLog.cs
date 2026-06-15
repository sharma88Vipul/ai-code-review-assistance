namespace AIReviewAssistant.Domain
{
    public class AuditLog
    {
        public Guid Id { get; private set; }
        public Guid OrganizationId { get; private set; }
        public Guid? UserId { get; private set; }
        public string Action { get; private set; } = string.Empty;
        public string EntityType { get; private set; } = string.Empty;
        public string EntityId { get; private set; } = string.Empty;
        public string? Details { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        private AuditLog()
        {
        }

        public static AuditLog Create(
            Guid organizationId,
            Guid? userId,
            string action,
            string entityType,
            string entityId,
            string? details = null)
        {
            if (organizationId == Guid.Empty)
                throw new ArgumentException("Organization id is required.", nameof(organizationId));

            if (userId == Guid.Empty)
                throw new ArgumentException("User id cannot be empty when provided.", nameof(userId));

            return new AuditLog
            {
                Id = Guid.NewGuid(),
                OrganizationId = organizationId,
                UserId = userId,
                Action = Required(action, nameof(action)),
                EntityType = Required(entityType, nameof(entityType)),
                EntityId = Required(entityId, nameof(entityId)),
                Details = string.IsNullOrWhiteSpace(details) ? null : details.Trim(),
                CreatedAtUtc = DateTime.UtcNow
            };
        }

        private static string Required(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value is required.", parameterName);

            return value.Trim();
        }
    }
}
