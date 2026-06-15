namespace AIReviewAssistant.Domain
{
    public interface IDomainEvent
    {
        DateTime OccurredAtUtc { get; }
    }
}
