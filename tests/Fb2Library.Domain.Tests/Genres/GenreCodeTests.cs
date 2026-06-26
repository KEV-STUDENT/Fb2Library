using FluentAssertions;
using Fb2Library.Domain.Genres;

namespace Fb2Library.Domain.Tests.Genres
{
    public class GenreCodeTests
    {
        [Fact]
        public void Create_WithValidCode_ShouldSetProperties()
        {
            // Arrange & Act
            var code = GenreCode.Create("sf");

            // Assert
            code.Value.Should().Be("sf");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidCode_ShouldThrowArgumentException(string code)
        {
            // Act
            Func<GenreCode> act = () => GenreCode.Create(code);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("*value*");
        }

        [Theory]
        [InlineData("sF", "sf")]
        [InlineData("SF", "sf")]
        [InlineData("Sf", "sf")]
        public void Code_ShouldFormatCorrectly(string actual, string expected)
        {
            // Act
            var name = GenreCode.Create(actual);

            // Assert
            name.Value.Should().Be(expected);
        }

        [Fact]
        public void TwoInstances_WithSameValues_ShouldBeEqual()
        {
            // Arrange
            var name1 = GenreCode.Create("sf");
            var name2 = GenreCode.Create("sf");

            // Assert
            name1.Should().Be(name2);
            name1.GetHashCode().Should().Be(name2.GetHashCode());
        }

        [Fact]
        public void TwoInstances_WithDifferentValues_ShouldNotBeEqual()
        {
            // Arrange
            var name1 = GenreCode.Create("sf");
            var name2 = GenreCode.Create("dt");

            // Assert
            name1.Should().NotBe(name2);
        }

        [Fact]
        public void ToString_ShouldBeValue()
        {
            // Arrange & Act
            var code = GenreCode.Create("sf");

            // Assert
            code.ToString().Should().Be("sf");
        }
    }
}
