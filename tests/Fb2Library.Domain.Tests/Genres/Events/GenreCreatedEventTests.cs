using Fb2Library.Domain.Genres;
using Fb2Library.Domain.Genres.Events;
namespace Fb2Library.Domain.Tests.Genres.Events
{
    public class GenreCreatedEventTests
    {
        [Fact]
        public void Create_WithBookId_ShouldSetBookId()
        {
            var event1 = new GenreCreatedEvent(GenreId.New());
            Assert.NotEqual(default, event1.GenreId);
            Assert.NotEqual(Guid.Empty, event1.GenreId.Value);
        }
    }
}
