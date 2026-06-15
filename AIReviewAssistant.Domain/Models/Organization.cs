using System;
using System.Collections.Generic;
using System.Text;

namespace AIReviewAssistant.Domain
{
    public class Organization
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public DateTime CreatedAtUtc { get; private set; }

        private Organization()
        {
        }

        public static Organization Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Organization name is required.", nameof(name));

            return new Organization
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                CreatedAtUtc = DateTime.UtcNow
            };
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Organization name is required.", nameof(name));

            Name = name.Trim();
        }
    }
}
