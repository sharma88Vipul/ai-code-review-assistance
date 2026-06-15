namespace AIReviewAssistant.Domain
{
    public sealed record ReviewStarted(
        Guid ReviewSessionId,
        Guid PullRequestId,
        DateTime OccurredAtUtc) : IDomainEvent;
}
