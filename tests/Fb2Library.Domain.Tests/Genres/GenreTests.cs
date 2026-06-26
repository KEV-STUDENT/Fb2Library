
using Fb2Library.Domain.Exceptions;
using Fb2Library.Domain.Genres;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Genres
{
    public class GenreTests
    {
        [Fact]
        public void Create_WithValidCode_ShouldSetProperties()
        {
            // Arrange
            var name = GenreCode.Create("sf");

            // Act
            var genre = Genre.Create("sf", "since fantastic");

            // Assert
            genre.Should().BeOfType<Genre>().And.NotBeNull();
            genre.Id.Value.Should().NotBeEmpty();
            genre.Code.Should().Be(name);
            genre.Description.Should().Be("since fantastic");
        }

        [Fact]
        public void Create_TwoNewGenres_IdsAreNotEqual()
        {
            // Arrange & Act
            var genre1 = Genre.Create("Test1", "Test1");
            var genre2 = Genre.Create("Test2", "Test2");

            // Assert
            genre1.Id.Should().NotBe(genre2.Id);
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
