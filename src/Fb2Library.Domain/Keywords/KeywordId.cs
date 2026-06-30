using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Keywords
{
    public sealed record KeywordId : GuidIdentity, IIdentityFabric<KeywordId, Guid>
    {
        public KeywordId(Guid value) : base(value) { }
        public static KeywordId From(Guid value) => new(value);
        public static KeywordId New() => new(Guid.NewGuid());

        public static implicit operator KeywordId(Guid value) => From(value);
    }
}
