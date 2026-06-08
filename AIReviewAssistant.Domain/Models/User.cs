using System;
using System.Collections.Generic;
using System.Text;

namespace AIReviewAssistant.Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public Guid OrganizationId { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string GitHubUsername { get; private set; } = string.Empty;
        public DateTime CreatedAtUtc { get; private set; }
    }
}
