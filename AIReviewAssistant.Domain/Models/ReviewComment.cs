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
    }
}
