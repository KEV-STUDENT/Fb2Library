using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Publishers
{
    public sealed record PublisherId : GuidIdentity, IIdentityFabric<PublisherId, Guid>
    {
        public PublisherId(Guid value) : base(value) { }
        public static PublisherId From(Guid value) => new(value);
        public static PublisherId New() => new(Guid.NewGuid());

        public static implicit operator PublisherId(Guid value) => From(value);
    }
}
