namespace AIReviewAssistant.Domain
{
    public class Notification
    {
        public Guid Id { get; private set; }
        public Guid OrganizationId { get; private set; }
        public Guid? UserId { get; private set; }
        public NotificationType Type { get; private set; }
        public string Subject { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public NotificationStatus Status { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? SentAtUtc { get; private set; }
        public string? ErrorMessage { get; private set; }

        private Notification()
        {
        }

        public static Notification Create(
            Guid organizationId,
            Guid? userId,
            NotificationType type,
            string subject,
            string message)
        {
            if (organizationId == Guid.Empty)
                throw new ArgumentException("Organization id is required.", nameof(organizationId));

            if (userId == Guid.Empty)
                throw new ArgumentException("User id cannot be empty when provided.", nameof(userId));

            return new Notification
            {
                Id = Guid.NewGuid(),
                OrganizationId = organizationId,
                UserId = userId,
                Type = type,
                Subject = Required(subject, nameof(subject)),
                Message = Required(message, nameof(message)),
                Status = NotificationStatus.Pending,
                CreatedAtUtc = DateTime.UtcNow
            };
        }

        public void MarkSent()
        {
            EnsurePending();
            Status = NotificationStatus.Sent;
            SentAtUtc = DateTime.UtcNow;
        }

        public void MarkFailed(string errorMessage)
        {
            EnsurePending();

            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentException("Error message is required.", nameof(errorMessage));

            Status = NotificationStatus.Failed;
            ErrorMessage = errorMessage.Trim();
        }

        private void EnsurePending()
        {
            if (Status != NotificationStatus.Pending)
                throw new InvalidOperationException("Only pending notifications can be finalized.");
        }

        private static string Required(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value is required.", parameterName);

            return value.Trim();
        }
    }
}
