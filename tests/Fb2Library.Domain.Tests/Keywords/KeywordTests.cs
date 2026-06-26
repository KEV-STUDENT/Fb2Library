using Fb2Library.Domain.Exceptions;
using Fb2Library.Domain.Keywords;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Keywords
{
    public class KeywordTests : EntityTests<Keyword, KeywordId>
    {
        protected override Keyword CreateEntity() => Keyword.Create("Test");

        [Fact]
        public void Create_WithValidCode_ShouldSetProperties()
        {
            // Arrange
            var word = KeywordWord.Create("test");

            // Act
            var kw = Keyword.Create("test");

            // Assert
            kw.Word.Should().Be(word);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidCode_ShouldThrowDomainException(string word)
        {
            // Act
            Func<Keyword> act = () => Keyword.Create(word);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("*Keyword must be specified*");
        }
    }
}
