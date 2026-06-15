using AIReviewAssistant.Domain;

namespace AIReviewAssistant.Domain.Tests;

public class ReviewSessionTests
{
    [Fact]
    public void Start_InitializesRunningSession()
    {
        var pullRequestId = Guid.NewGuid();

        var session = ReviewSession.Start(pullRequestId);

        Assert.NotEqual(Guid.Empty, session.Id);
        Assert.Equal(pullRequestId, session.PullRequestId);
        Assert.Equal(ReviewStatus.Running, session.Status);
        Assert.Null(session.CompletedAtUtc);
    }

    [Fact]
    public void Start_WithoutPullRequest_Throws()
    {
        Assert.Throws<ArgumentException>(() => ReviewSession.Start(Guid.Empty));
    }

    [Fact]
    public void Complete_StoresMetricsAndCompletionTime()
    {
        var session = ReviewSession.Start(Guid.NewGuid());

        session.Complete(12, 4);

        Assert.Equal(ReviewStatus.Completed, session.Status);
        Assert.Equal(12, session.FilesAnalyzed);
        Assert.Equal(4, session.TotalComments);
        Assert.NotNull(session.CompletedAtUtc);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    public void Complete_WithNegativeMetrics_Throws(int filesAnalyzed, int totalComments)
    {
        var session = ReviewSession.Start(Guid.NewGuid());

        Assert.Throws<ArgumentOutOfRangeException>(
            () => session.Complete(filesAnalyzed, totalComments));
    }

    [Fact]
    public void Fail_StoresTrimmedError()
    {
        var session = ReviewSession.Start(Guid.NewGuid());

        session.Fail("  Analyzer timed out.  ");

        Assert.Equal(ReviewStatus.Failed, session.Status);
        Assert.Equal("Analyzer timed out.", session.ErrorMessage);
        Assert.NotNull(session.CompletedAtUtc);
    }

    [Fact]
    public void Complete_AfterFailure_Throws()
    {
        var session = ReviewSession.Start(Guid.NewGuid());
        session.Fail("Analyzer timed out.");

        Assert.Throws<InvalidOperationException>(() => session.Complete(1, 0));
    }
}
