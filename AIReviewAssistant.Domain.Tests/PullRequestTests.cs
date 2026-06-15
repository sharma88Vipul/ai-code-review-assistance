using AIReviewAssistant.Domain;

namespace AIReviewAssistant.Domain.Tests;

public class PullRequestTests
{
    [Fact]
    public void Create_InitializesOpenPullRequest()
    {
        var pullRequest = CreatePullRequest();

        Assert.NotEqual(Guid.Empty, pullRequest.Id);
        Assert.Equal(PullRequestStatus.Open, pullRequest.Status);
        Assert.Equal(17, pullRequest.PullRequestNumber);
    }

    [Fact]
    public void Create_WithNonPositiveNumber_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => PullRequest.Create(
            Guid.NewGuid(),
            0,
            "Improve review workflow",
            "feature/review",
            "main",
            "developer",
            "https://github.com/acme/repo/pull/17"));
    }

    [Fact]
    public void ReviewLifecycle_TransitionsInOrder()
    {
        var pullRequest = CreatePullRequest();

        pullRequest.StartProcessing();
        Assert.Equal(PullRequestStatus.Processing, pullRequest.Status);

        pullRequest.MarkReviewed();
        Assert.Equal(PullRequestStatus.Reviewed, pullRequest.Status);

        pullRequest.Close();
        Assert.Equal(PullRequestStatus.Closed, pullRequest.Status);
    }

    [Fact]
    public void MarkReviewed_BeforeProcessing_Throws()
    {
        var pullRequest = CreatePullRequest();

        Assert.Throws<InvalidOperationException>(pullRequest.MarkReviewed);
    }

    [Fact]
    public void Close_WhenAlreadyClosed_Throws()
    {
        var pullRequest = CreatePullRequest();
        pullRequest.Close();

        Assert.Throws<InvalidOperationException>(pullRequest.Close);
    }

    private static PullRequest CreatePullRequest()
    {
        return PullRequest.Create(
            Guid.NewGuid(),
            17,
            "Improve review workflow",
            "feature/review",
            "main",
            "developer",
            "https://github.com/acme/repo/pull/17");
    }
}
