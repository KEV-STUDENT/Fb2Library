using Fb2Library.Domain.Shared.Interfaces;
using Fb2Library.Infrastructure.Parsing;
using FluentAssertions;

namespace Fb2Library.Integration.Tests;

public class DocumentParserFactoryTests
{
    [Theory]
    [InlineData("test1.fb2")]
    [InlineData("test1.FB2")]
    [InlineData("test1.fBP")]
    [InlineData("test1.Fb2")]
    public void DocumentParserFactory_CorrectFileName_ShouldBe_IDocumentParser(string fileName)
    {
        var factory = new DocumentParserFactory([new Fb2Parser()]);
        IDocumentParser parser = factory.GetDocumentParser(fileName);

        parser.Should().NotBeNull();
        parser.Should().BeAssignableTo<IDocumentParser>();
    }


    [Theory]
    [InlineData("test1.fbX")]
    public void DocumentParserFactory_InvalidFileName_ThrowInvalidOperationExceptio(string fileName)
    {
        var factory = new DocumentParserFactory([new Fb2Parser()]);

        // Act
        Action action = () => factory.GetDocumentParser(fileName);

        // Assert
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("*can't be parsed*");
    }
}
