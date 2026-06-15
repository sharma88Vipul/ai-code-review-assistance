namespace AIReviewAssistant.Domain
{
    public sealed record ReviewCompleted(
        Guid ReviewSessionId,
        Guid PullRequestId,
        int FilesAnalyzed,
        int TotalComments,
        DateTime OccurredAtUtc) : IDomainEvent;
}
