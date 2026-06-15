namespace AIReviewAssistant.Domain
{
    public sealed record PullRequestCreated(
        Guid PullRequestId,
        Guid RepositoryId,
        int PullRequestNumber,
        DateTime OccurredAtUtc) : IDomainEvent;
}
