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
    }
}
