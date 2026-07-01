using Fb2Library.Domain.Books;
using Fb2Library.Domain.Tests.Shared;

namespace Fb2Library.Domain.Tests.Books
{
    public class BookTests : EntityTests<Book, BookId, BookInfo>
    {
        protected override Book CreateEntity() => Book.Create("Test", 1990, "Москва");
        protected override BookInfo CreateValueObject() => BookInfo.Create("Test", 1990, "Москва");
    }
}
