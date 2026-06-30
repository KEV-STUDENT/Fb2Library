using Fb2Library.Domain.Books;
using Fb2Library.Domain.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests
{
    public class AggregateRootTests
    {
        [Fact]
        public void DomainEvents_Incremented()
        {
            AggregateRoot<BookId, BookInfo> book = Book.Create("Test", 1990, "Москва"); ;
            book.DomainEvents.Should().HaveCount(1);
        }
    }
}
