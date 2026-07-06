using Fb2Library.Domain.Shared;
using Fb2Library.Domain.Shared.Interfaces;
namespace Fb2Library.Domain.Books
{
    public sealed record BookId : GuidIdentity, IIdentityFabric<BookId, Guid>
    {
        public BookId(Guid value) : base(value) {}

        public static BookId From(Guid value) => new(value);
        public static BookId New() => From(Guid.NewGuid());

        public static implicit operator BookId(Guid value) => From(value);
    }
}
