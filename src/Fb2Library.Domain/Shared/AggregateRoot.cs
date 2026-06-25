

namespace Fb2Library.Domain.Shared
{
    public abstract class AggregateRoot<TId> : Entity<TId>
        where TId : IntIdentity
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected AggregateRoot(TId id) : base(id) { }
        protected AggregateRoot() { } // For EF Core

        public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
