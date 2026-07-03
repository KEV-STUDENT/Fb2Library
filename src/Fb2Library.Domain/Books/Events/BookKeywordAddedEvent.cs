using Fb2Library.Domain.Books.Entities.Keywords;
using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Books.Events
{
    public sealed record class BookKeywordAddedEvent(BookId BookId, KeywordId KeywordId) : AddEvent<BookId, KeywordId>(BookId, KeywordId)
    {
    }
}
