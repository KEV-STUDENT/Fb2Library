using Fb2Library.Domain.Shared;
using Fb2Library.Domain.Shared.Interfaces;

namespace Fb2Library.Domain
{
    public sealed record DomainEventId : GuidIdentity, IIdentityFabric<DomainEventId, Guid>
    {
        public DomainEventId(Guid original) : base(original)
        {
        }
        public static DomainEventId From(Guid value) => new(value);
        public static DomainEventId New() => new(Guid.NewGuid());
        public static implicit operator DomainEventId(Guid value) => From(value);
    }
}
