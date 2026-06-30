using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Genres.Events
{
    public sealed record GenreCreatedEvent(GenreId GenreId) : CreateEvent<GenreId>(GenreId) { }
}
