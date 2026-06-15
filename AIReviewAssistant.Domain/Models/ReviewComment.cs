using System;
using System.Collections.Generic;
using System.Text;

namespace AIReviewAssistant.Domain
{
    public class ReviewComment
    {
        public Guid Id { get; private set; }
        public Guid ReviewSessionId { get; private set; }

        public string FilePath { get; private set; } = string.Empty;
        public int? LineNumber { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public ReviewCommentSeverity Severity { get; private set; }
        public ReviewCommentCategory Category { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }

        private ReviewComment()
        {
        }

        public static ReviewComment Create(
            Guid reviewSessionId,
            string filePath,
            int? lineNumber,
            string message,
            ReviewCommentSeverity severity,
            ReviewCommentCategory category)
        {
            if (reviewSessionId == Guid.Empty)
                throw new ArgumentException("Review session id is required.", nameof(reviewSessionId));

            if (lineNumber.HasValue && lineNumber.Value <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(lineNumber),
                    "Line number must be greater than zero when provided.");

            return new ReviewComment
            {
                Id = Guid.NewGuid(),
                ReviewSessionId = reviewSessionId,
                FilePath = Required(filePath, nameof(filePath)),
                LineNumber = lineNumber,
                Message = Required(message, nameof(message)),
                Severity = severity,
                Category = category,
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
