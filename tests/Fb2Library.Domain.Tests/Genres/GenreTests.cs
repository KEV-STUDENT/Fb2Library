
using Fb2Library.Domain.Exceptions;
using Fb2Library.Domain.Genres;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Genres
{
    public class GenreTests : EntityTests<Genre, GenreId>
    {
        protected override Genre CreateEntity() => Genre.Create("Test1", "Test1");

        [Fact]
        public void Create_WithValidCode_ShouldSetProperties()
        {
            // Arrange
            var name = GenreCode.Create("sf");

            // Act
            var genre = Genre.Create("sf", "since fantastic");

            // Assert
            genre.Code.Should().Be(name);
            genre.Description.Should().Be("since fantastic");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidCode_ShouldThrowDomainException(string code)
        {
            // Act
            Func<Genre> act = () => Genre.Create(code);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("*Code for Genre must be specify*");
        }

        [Fact]
        public void Create_InvalidDescription_ShouldArgumentNullException()
        {
            // Act
            Func<Genre> act = () => Genre.Create("sf", null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("*Description is null*");
        }
    }
}
