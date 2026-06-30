using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Books
{
    public sealed class Book : AggregateRoot<BookId, BookInfo>
    {
        public Book(BookInfo vo) : base(vo) { }
        public static Book Create(string title, uint? year, string? city) => new(BookInfo.Create(title, year, city));
        protected override BookId GetNewId() => BookId.New();
    }
}
