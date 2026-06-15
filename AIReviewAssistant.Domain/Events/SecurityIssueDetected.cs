namespace AIReviewAssistant.Domain
{
    public sealed record SecurityIssueDetected(
        Guid SecurityFindingId,
        Guid ReviewSessionId,
        SecuritySeverity Severity,
        DateTime OccurredAtUtc) : IDomainEvent;
}
