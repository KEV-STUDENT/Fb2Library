using Fb2Library.Domain.Keywords;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Keywords
{
    public class KeywordWordTests
    {
        [Fact]
        public void Create_WithValidWord_ShouldSetProperties()
        {
            // Arrange & Act
            var code = KeywordWord.Create("sf");

            // Assert
            code.Value.Should().Be("sf");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidWord_ShouldThrowArgumentException(string code)
        {
            // Act
            Func<KeywordWord> act = () => KeywordWord.Create(code);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("*value*");
        }

        [Theory]
        [InlineData("sF", "sf")]
        [InlineData("SF", "sf")]
        [InlineData("Sf", "sf")]
        [InlineData("  sF", "sf")]
        [InlineData("sF   ", "sf")]
        public void Word_ShouldFormatCorrectly(string actual, string expected)
        {
            // Act
            var name = KeywordWord.Create(actual);

            // Assert
            name.Value.Should().Be(expected);
        }

        [Fact]
        public void TwoInstances_WithSameValues_ShouldBeEqual()
        {
            // Arrange
            var name1 = KeywordWord.Create("sf");
            var name2 = KeywordWord.Create("sf");

            // Assert
            name1.Should().Be(name2);
            name1.GetHashCode().Should().Be(name2.GetHashCode());
        }

        [Fact]
        public void TwoInstances_WithDifferentValues_ShouldNotBeEqual()
        {
            // Arrange
            var name1 = KeywordWord.Create("sf");
            var name2 = KeywordWord.Create("dt");

            // Assert
            name1.Should().NotBe(name2);
        }

        [Fact]
        public void ToString_ShouldBeValue()
        {
            // Arrange & Act
            var code = KeywordWord.Create("sf");

            // Assert
            code.ToString().Should().Be("sf");
        }
    }
}
