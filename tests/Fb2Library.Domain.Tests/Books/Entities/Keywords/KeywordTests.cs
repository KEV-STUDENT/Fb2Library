using Fb2Library.Domain.Books.Entities.Keywords;
using Fb2Library.Domain.Exceptions;
using Fb2Library.Domain.Tests.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Books.Entities.Keywords
{
    public class KeywordTests : EntityTests<Keyword, KeywordId, KeywordWord>
    {
        protected override Keyword CreateEntity() => Keyword.Create("test");
        protected override KeywordWord CreateValueObject() => KeywordWord.Create("test");


        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidCode_ShouldThrowDomainException(string word)
        {
            // Act
            Func<Keyword> act = () => Keyword.Create(word);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("*must be specified*");
        }
    }
}
