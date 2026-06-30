using Fb2Library.Domain.Books;
using Fb2Library.Domain.Books.Events;

namespace Fb2Library.Domain.Tests.Books.Events
{
    public class BookCreatedEventTests
    {
        [Fact]
        public void Create_WithBookId_ShouldSetBookId()
        {
            var event1 = new BookCreatedEvent(BookId.New());
            Assert.NotEqual(default, event1.BookId);
            Assert.NotEqual(Guid.Empty, event1.BookId.Value);
        }
    }
}
