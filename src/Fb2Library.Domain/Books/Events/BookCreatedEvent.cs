using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Books.Events
{
    public sealed record BookCreatedEvent(BookId BookId) : CreateEvent<BookId>(BookId) { }
}
