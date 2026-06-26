using Fb2Library.Domain.Genres;

namespace Fb2Library.Domain.Tests.Genres
{
    public class GenreIdTests : GuidIdentityTests<GenreId>
    {
        protected override GenreId ConvertFromGuid(Guid value)
        {
            GenreId id = value;
            return id;
        }
        protected override GenreId CreateFrom(Guid value) => GenreId.From(value);
        protected override GenreId CreateNew() => GenreId.New();
    }
}

