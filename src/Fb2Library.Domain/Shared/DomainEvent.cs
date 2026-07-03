namespace Fb2Library.Domain.Shared
{
    public abstract record DomainEvent
    {
        public  DomainEventId Id { get; init; } = DomainEventId.New();
        public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

        public virtual bool Equals(DomainEvent? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            // Сравниваем только бизнес-свойства
            return GetBusinessEqualityComponents().SequenceEqual(other.GetBusinessEqualityComponents());
        }

        protected abstract IEnumerable<object> GetBusinessEqualityComponents();

        public override int GetHashCode()
        {
            return GetBusinessEqualityComponents()
                .Aggregate(0, (hash, component) => HashCode.Combine(hash, component?.GetHashCode() ?? 0));
        }
    }

    public abstract record EntityEvent<TId> : DomainEvent
        where TId : notnull
    {
        public EntityEvent(TId entityId)
        {
            if (entityId == null) throw new ArgumentNullException(nameof(entityId));
            EntityId = entityId;
        }

        public TId EntityId { get; init; }

        protected override IEnumerable<object> GetBusinessEqualityComponents()
        {
            yield return EntityId;
            // EventId и OccurredOn не участвуют в сравнении
        }
    }
    public abstract record CreateEvent<TId>(TId EntityId) : EntityEvent<TId>(EntityId) where TId : notnull;
    public abstract record UpdateEvent<TId>(TId EntityId) : EntityEvent<TId>(EntityId) where TId : notnull;
    public abstract record DeleteEvent<TId>(TId EntityId) : EntityEvent<TId>(EntityId) where TId : notnull;
    public abstract record AssignEvent<TId>(TId EntityId) : EntityEvent<TId>(EntityId) where TId : notnull;

    public abstract record EntitiesLinkEvent<TId1, TId2> : DomainEvent
        where TId1 : notnull
        where TId2 : notnull
    {
        public EntitiesLinkEvent(TId1 entityId1, TId2 entityId2)
        {
            if (entityId1 == null) throw new ArgumentNullException(nameof(entityId1));
            if (entityId2 == null) throw new ArgumentNullException(nameof(entityId2));

            EntityId1 = entityId1;
            EntityId2 = entityId2;
        }

        public TId1 EntityId1 { get; init; }
        public TId2 EntityId2 { get; init; }

        protected override IEnumerable<object> GetBusinessEqualityComponents()
        {
            yield return EntityId1;
            yield return EntityId2;
            // EventId и OccurredOn не участвуют в сравнении
        }
    }

    public abstract record AddEvent<TId1, TId2>(TId1 EntityId1, TId2 EntityId2) : EntitiesLinkEvent<TId1, TId2>(EntityId1, EntityId2)
        where TId1 : notnull where TId2 : notnull;
}
