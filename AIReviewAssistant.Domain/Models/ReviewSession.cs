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
        private ReviewSession()
        {
        }

        public static ReviewSession Start(Guid pullRequestId)
        {
            if (pullRequestId == Guid.Empty)
                throw new ArgumentException("Pull request id is required.");

            return new ReviewSession
            {
                Id = Guid.NewGuid(),
                PullRequestId = pullRequestId,
                Status = ReviewStatus.Running,
                StartedAtUtc = DateTime.UtcNow
            };
        }

        public void Complete(int filesAnalyzed, int totalComments)
        {
            EnsureRunning();

            if (filesAnalyzed < 0)
                throw new ArgumentOutOfRangeException(nameof(filesAnalyzed));

            if (totalComments < 0)
                throw new ArgumentOutOfRangeException(nameof(totalComments));

            Status = ReviewStatus.Completed;
            FilesAnalyzed = filesAnalyzed;
            TotalComments = totalComments;
            CompletedAtUtc = DateTime.UtcNow;
        }

        public void Fail(string errorMessage)
        {
            EnsureRunning();

            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentException("Error message is required.", nameof(errorMessage));

            Status = ReviewStatus.Failed;
            ErrorMessage = errorMessage.Trim();
            CompletedAtUtc = DateTime.UtcNow;
        }

        private void EnsureRunning()
        {
            if (Status != ReviewStatus.Running)
                throw new InvalidOperationException("Only running review sessions can be finalized.");
        }
    }
}
