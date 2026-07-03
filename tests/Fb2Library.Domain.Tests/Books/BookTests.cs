using Fb2Library.Domain.Books;
using Fb2Library.Domain.Books.Entities.Keywords;
using Fb2Library.Domain.Tests.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Books
{
    public class BookTests : EntityTests<Book, BookId, BookInfo>
    {
        protected override Book CreateEntity() => Book.Create("Test", 1990, "Москва");
        protected override BookInfo CreateValueObject() => BookInfo.Create("Test", 1990, "Москва");

        [Fact]
        public void AddKeyword_DomainEvents_Incremented()
        {
            var book = Book.Create("Test", 1990, "Москва");
            int count = book.DomainEvents.Count;

            book.AddKeyword(Keyword.Create("Test1").Id);
            count++;

            book.DomainEvents.Count.Should().Be(count);
        }
    }
}
