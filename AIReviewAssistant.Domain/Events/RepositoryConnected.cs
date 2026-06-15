namespace AIReviewAssistant.Domain
{
    public sealed record RepositoryConnected(
        Guid RepositoryId,
        Guid OrganizationId,
        DateTime OccurredAtUtc) : IDomainEvent;
}
