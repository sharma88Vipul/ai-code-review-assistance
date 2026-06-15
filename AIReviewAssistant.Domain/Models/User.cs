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

        private User()
        {
        }

        public static User Create(
            Guid organizationId,
            string fullName,
            string email,
            string gitHubUsername)
        {
            if (organizationId == Guid.Empty)
                throw new ArgumentException("Organization id is required.", nameof(organizationId));

            return new User
            {
                Id = Guid.NewGuid(),
                OrganizationId = organizationId,
                FullName = Required(fullName, nameof(fullName)),
                Email = Required(email, nameof(email)).ToLowerInvariant(),
                GitHubUsername = Required(gitHubUsername, nameof(gitHubUsername)),
                CreatedAtUtc = DateTime.UtcNow
            };
        }

        public void UpdateProfile(string fullName, string email)
        {
            FullName = Required(fullName, nameof(fullName));
            Email = Required(email, nameof(email)).ToLowerInvariant();
        }

        private static string Required(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value is required.", parameterName);

            return value.Trim();
        }
    }
}
