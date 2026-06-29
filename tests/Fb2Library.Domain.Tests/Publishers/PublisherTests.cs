using Fb2Library.Domain.Exceptions;
using Fb2Library.Domain.Publishers;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Publishers
{
    public class PublisherTests : EntityTests<Publisher, PublisherId, PublisherName>
    {
        protected override Publisher CreateEntity() => Publisher.Create("Test");
        protected override PublisherName CreateValueObject() => PublisherName.Create("Test");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidName_ShouldThrowDomainException(string word)
        {
            // Act
            Func<Publisher> act = () => Publisher.Create(word);

            // Assert
            act.Should().Throw<DomainException>()
                .WithMessage("*must be specified*");
        }
    }
}
