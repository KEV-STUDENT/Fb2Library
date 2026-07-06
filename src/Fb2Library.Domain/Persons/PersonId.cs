using Fb2Library.Domain.Shared;
using Fb2Library.Domain.Shared.Interfaces;

namespace Fb2Library.Domain.Persons
{
    public sealed record PersonId : GuidIdentity, IIdentityFabric<PersonId, Guid>
    {
        public PersonId(Guid value) : base(value) { }

        public static PersonId From(Guid value) => new(value);
        public static PersonId New() => new(Guid.NewGuid());

        public static implicit operator PersonId(Guid value) => From(value);
    }
}
