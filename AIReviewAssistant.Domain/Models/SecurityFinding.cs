using System;
using System.Collections.Generic;
using System.Text;

namespace AIReviewAssistant.Domain
{
    public class SecurityFinding
    {
        public Guid Id { get; private set; }
        public Guid ReviewSessionId { get; private set; }

        public string FilePath { get; private set; } = string.Empty;
        public int? LineNumber { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public SecuritySeverity Severity { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }

        private SecurityFinding()
        {
        }

        public static SecurityFinding Create(
            Guid reviewSessionId,
            string filePath,
            int? lineNumber,
            string title,
            string description,
            SecuritySeverity severity)
        {
            if (reviewSessionId == Guid.Empty)
                throw new ArgumentException("Review session id is required.", nameof(reviewSessionId));

            if (lineNumber.HasValue && lineNumber.Value <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(lineNumber),
                    "Line number must be greater than zero when provided.");

            return new SecurityFinding
            {
                Id = Guid.NewGuid(),
                ReviewSessionId = reviewSessionId,
                FilePath = Required(filePath, nameof(filePath)),
                LineNumber = lineNumber,
                Title = Required(title, nameof(title)),
                Description = Required(description, nameof(description)),
                Severity = severity,
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
