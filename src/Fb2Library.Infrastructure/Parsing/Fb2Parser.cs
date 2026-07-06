using Fb2Library.Domain.Books;
using Fb2Library.Domain.Shared.Enums;
using Fb2Library.Domain.Shared.Interfaces;

namespace Fb2Library.Infrastructure.Parsing
{
    public class Fb2Parser : IDocumentParser
    {
        public DocumentFormat SupportedFormat => DocumentFormat.FB2;

        public bool CanParse(string fileName) => throw new NotImplementedException();
        public Task<Book> ParseAsync(Stream fileStream, string fileName) => throw new NotImplementedException();
    }
}
