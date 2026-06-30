using Fb2Library.Domain.Books;

namespace Fb2Library.Domain.Tests.Books
{
    public class BookIdTests : GuidIdentityTests<BookId>
    {
        protected override BookId ConvertFromGuid(Guid value)
        {
            BookId id = value;
            return id;
        }
        protected override BookId CreateFrom(Guid value) => BookId.From(value);
        protected override BookId CreateNew() => BookId.New();
    }
}
