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
    }
}
