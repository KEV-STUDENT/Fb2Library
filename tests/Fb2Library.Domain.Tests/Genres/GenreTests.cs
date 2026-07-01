
using Fb2Library.Domain.Exceptions;
using Fb2Library.Domain.Genres;
using Fb2Library.Domain.Tests.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Genres
{
    public class GenreTests : EntityTests<Genre, GenreId, GenreCode>
    {
        protected override Genre CreateEntity() => Genre.Create("sf", "since fantastic");
        protected override GenreCode CreateValueObject() => GenreCode.Create("sf");

        [Fact]
        public void Create_WithValidCode_ShouldSetProperties()
        {
            // Arrange & Act
            Genre genre = CreateEntity();

            // Assert
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
                .WithMessage("*must be specify*");
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
