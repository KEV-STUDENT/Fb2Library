namespace Fb2Library.Domain.Shared
{
    public abstract record DomainEvent
    {
        public  DomainEventId Id { get; init; } = DomainEventId.New();
        public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    }
}
