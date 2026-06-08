using System;
using System.Collections.Generic;
using System.Text;

namespace AIReviewAssistant.Domain
{
    public class PullRequest
    {
        public Guid Id { get; private set; }
        public Guid RepositoryId { get; private set; }

        public int PullRequestNumber { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string SourceBranch { get; private set; } = string.Empty;
        public string TargetBranch { get; private set; } = string.Empty;
        public string Author { get; private set; } = string.Empty;
        public string Url { get; private set; } = string.Empty;

        public PullRequestStatus Status { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
    }
}
