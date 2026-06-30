namespace Fb2Library.Domain.Shared
{
    public abstract record DomainEvent
    {
        public  DomainEventId Id { get; init; } = DomainEventId.New();
        public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    }

    public abstract record EntityEvent<TId> : DomainEvent
    {
        public EntityEvent(TId entityId)
        {
            if (entityId == null) throw new ArgumentNullException(nameof(entityId));
            EntityId = entityId;
        }

        public TId EntityId { get; init; }
    }
    public abstract record CreateEvent<TId>(TId EntityId) : EntityEvent<TId>(EntityId);
    public abstract record UpdateEvent<TId>(TId EntityId) : EntityEvent<TId>(EntityId);
    public abstract record DeleteEvent<TId>(TId EntityId) : EntityEvent<TId>(EntityId);

    public abstract record AssignEvent<TId>(TId EntityId) : EntityEvent<TId>(EntityId);
}
