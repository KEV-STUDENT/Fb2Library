using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Genres
{
    public sealed record GenreId : GuidIdentity, IIdentityFabric<GenreId, Guid>
    {
        public GenreId(Guid value) : base(value) { }

        public static GenreId From(Guid value) => new(value);
        public static GenreId New() => new(Guid.NewGuid());

        public static implicit operator GenreId(Guid value) => From(value);
    }
}
