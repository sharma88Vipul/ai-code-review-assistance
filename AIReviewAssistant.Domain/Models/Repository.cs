using System;
using System.Collections.Generic;
using System.Text;

namespace AIReviewAssistant.Domain
{
    public class Repository
    {
        public Guid Id { get; private set; }
        public Guid OrganizationId { get; private set; }

        public string Name { get; private set; } = string.Empty;
        public string Owner { get; private set; } = string.Empty;
        public string GitHubRepositoryId { get; private set; } = string.Empty;
        public string Url { get; private set; } = string.Empty;
        public string DefaultBranch { get; private set; } = "main";

        public DateTime ConnectedAtUtc { get; private set; }
        public bool IsActive { get; private set; }
    }
}
