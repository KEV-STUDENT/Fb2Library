using Fb2Library.Domain.Books.Entities.Keywords;
using Fb2Library.Domain.Books.Events;
using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Books
{
    public sealed class Book : AggregateRoot<BookId, BookInfo>
    {
        public Book(BookInfo vo) : base(vo)
        {
            AddDomainEvent(new BookCreatedEvent(Id));
        }
        public static Book Create(string title, uint? year, string? city) => new(BookInfo.Create(title, year, city));
        public void AddKeyword(KeywordId id)
        {
            AddDomainEvent(new BookKeywordAddedEvent(Id, id));
        }
        protected override BookId GetNewId() => BookId.New();
    }
}
