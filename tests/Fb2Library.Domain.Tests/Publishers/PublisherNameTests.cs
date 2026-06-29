using FluentAssertions;
using Fb2Library.Domain.Publishers;

namespace Fb2Library.Domain.Tests.Publishers
{

    public class PublisherNameTests
    {
        [Fact]
        public void Create_WithValidWord_ShouldSetProperties()
        {
            // Arrange & Act
            var code = PublisherName.Create("sf");

            // Assert
            code.Value.Should().Be("sf");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidWord_ShouldThrowArgumentException(string code)
        {
            // Act
            Func<PublisherName> act = () => PublisherName.Create(code);

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
            var name = PublisherName.Create(actual);
            var exp = PublisherName.Create(expected);

            // Assert
            name.Should().Be(exp);
        }

        [Fact]
        public void TwoInstances_WithSameValues_ShouldBeEqual()
        {
            // Arrange
            var name1 = PublisherName.Create("sf");
            var name2 = PublisherName.Create("sf");

            // Assert
            name1.Should().Be(name2);
            name1.GetHashCode().Should().Be(name2.GetHashCode());
        }

        [Fact]
        public void TwoInstances_WithDifferentValues_ShouldNotBeEqual()
        {
            // Arrange
            var name1 = PublisherName.Create("sf");
            var name2 = PublisherName.Create("dt");

            // Assert
            name1.Should().NotBe(name2);
        }

        [Fact]
        public void ToString_ShouldBeValue()
        {
            // Arrange & Act
            var code = PublisherName.Create("sf");

            // Assert
            code.ToString().Should().Be("sf");
        }
    }
}
