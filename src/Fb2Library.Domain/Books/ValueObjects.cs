using Fb2Library.Domain.Books.Entities.Keywords;
using Fb2Library.Domain.Genres;
using Fb2Library.Domain.Persons;
using Fb2Library.Domain.Publishers;
using Fb2Library.Domain.Sequences;

namespace Fb2Library.Domain.Books
{
    public record BookAuthor(BookId BookId, PersonId AuthorId) { }
    public record BookGenre(BookId BookId, GenreId GenreId) { }
    public record BookKeyword(BookId BookId, KeywordId KeywordId) { }
    public record BookPublisher(BookId BookId, PublisherId PublisherId) { }
    public record BookSequence(BookId BookId, SequenceId SequenceId, ushort Valume) { }
}
