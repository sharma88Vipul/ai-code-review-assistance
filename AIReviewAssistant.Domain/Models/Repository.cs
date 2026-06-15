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

        private Repository()
        {
        }

        public static Repository Connect(
            Guid organizationId,
            string name,
            string owner,
            string gitHubRepositoryId,
            string url,
            string defaultBranch = "main")
        {
            if (organizationId == Guid.Empty)
                throw new ArgumentException("Organization id is required.", nameof(organizationId));

            return new Repository
            {
                Id = Guid.NewGuid(),
                OrganizationId = organizationId,
                Name = Required(name, nameof(name)),
                Owner = Required(owner, nameof(owner)),
                GitHubRepositoryId = Required(gitHubRepositoryId, nameof(gitHubRepositoryId)),
                Url = Required(url, nameof(url)),
                DefaultBranch = Required(defaultBranch, nameof(defaultBranch)),
                ConnectedAtUtc = DateTime.UtcNow,
                IsActive = true
            };
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        public void ChangeDefaultBranch(string defaultBranch)
        {
            DefaultBranch = Required(defaultBranch, nameof(defaultBranch));
        }

        private static string Required(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value is required.", parameterName);

            return value.Trim();
        }
    }
}
