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

        private PullRequest()
        {
        }

        public static PullRequest Create(
            Guid repositoryId,
            int pullRequestNumber,
            string title,
            string sourceBranch,
            string targetBranch,
            string author,
            string url)
        {
            if (repositoryId == Guid.Empty)
                throw new ArgumentException("Repository id is required.", nameof(repositoryId));

            if (pullRequestNumber <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(pullRequestNumber),
                    "Pull request number must be greater than zero.");

            return new PullRequest
            {
                Id = Guid.NewGuid(),
                RepositoryId = repositoryId,
                PullRequestNumber = pullRequestNumber,
                Title = Required(title, nameof(title)),
                SourceBranch = Required(sourceBranch, nameof(sourceBranch)),
                TargetBranch = Required(targetBranch, nameof(targetBranch)),
                Author = Required(author, nameof(author)),
                Url = Required(url, nameof(url)),
                Status = PullRequestStatus.Open,
                CreatedAtUtc = DateTime.UtcNow
            };
        }

        public void StartProcessing()
        {
            if (Status != PullRequestStatus.Open)
                throw new InvalidOperationException("Only open pull requests can start processing.");

            Status = PullRequestStatus.Processing;
        }

        public void MarkReviewed()
        {
            if (Status != PullRequestStatus.Processing)
                throw new InvalidOperationException("Only processing pull requests can be reviewed.");

            Status = PullRequestStatus.Reviewed;
        }

        public void Close()
        {
            if (Status == PullRequestStatus.Closed)
                throw new InvalidOperationException("Pull request is already closed.");

            Status = PullRequestStatus.Closed;
        }

        private static string Required(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value is required.", parameterName);

            return value.Trim();
        }
    }
}
