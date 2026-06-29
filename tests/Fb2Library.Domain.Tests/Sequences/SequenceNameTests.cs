using Fb2Library.Domain.Sequences;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Sequences
{
    public class SequenceNameTests
    {
        [Fact]
        public void Create_WithValidWord_ShouldSetProperties()
        {
            // Arrange & Act
            var code = SequenceName.Create("sf");

            // Assert
            code.Value.Should().Be("sf");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidWord_ShouldThrowArgumentException(string code)
        {
            // Act
            Func<SequenceName> act = () => SequenceName.Create(code);

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
            var name = SequenceName.Create(actual);
            var exp = SequenceName.Create(expected);

            // Assert
            name.Should().Be(exp);
        }

        [Fact]
        public void TwoInstances_WithSameValues_ShouldBeEqual()
        {
            // Arrange
            var name1 = SequenceName.Create("sf");
            var name2 = SequenceName.Create("sf");

            // Assert
            name1.Should().Be(name2);
            name1.GetHashCode().Should().Be(name2.GetHashCode());
        }

        [Fact]
        public void TwoInstances_WithDifferentValues_ShouldNotBeEqual()
        {
            // Arrange
            var name1 = SequenceName.Create("sf");
            var name2 = SequenceName.Create("dt");

            // Assert
            name1.Should().NotBe(name2);
        }

        [Fact]
        public void ToString_ShouldBeValue()
        {
            // Arrange & Act
            var code = SequenceName.Create("sf");

            // Assert
            code.ToString().Should().Be("sf");
        }
    }
}
