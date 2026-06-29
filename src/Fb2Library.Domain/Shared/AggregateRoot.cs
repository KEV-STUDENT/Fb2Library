

namespace Fb2Library.Domain.Shared
{
    public abstract class AggregateRoot<TId, TVo> : Entity<TId, TVo>
        where TId : GuidIdentity
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected AggregateRoot(TVo vo) : base(vo) { }
        protected AggregateRoot(TId id, TVo vo) : base(id, vo) { }
        //protected AggregateRoot() { } // For EF Core

        public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
