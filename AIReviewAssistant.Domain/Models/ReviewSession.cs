using System;
using System.Collections.Generic;
using System.Text;

namespace AIReviewAssistant.Domain
{
    public class ReviewSession
    {
        public Guid Id { get; private set; }
        public Guid PullRequestId { get; private set; }

        public ReviewStatus Status { get; private set; }
        public int FilesAnalyzed { get; private set; }
        public int TotalComments { get; private set; }

        public DateTime StartedAtUtc { get; private set; }
        public DateTime? CompletedAtUtc { get; private set; }
        public string? ErrorMessage { get; private set; }
    }
}
