using Fb2Library.Domain.Books;
using Fb2Library.Domain.Shared.Enums;
using Fb2Library.Domain.Shared;

namespace Fb2Library.Infrastructure.Parsing
{
    public class Fb2Parser : DocumentParser
    {
        public override DocumentFormat SupportedFormat => DocumentFormat.FB2;
        public override Task<Book> ParseAsync(Stream fileStream, string fileName) => throw new NotImplementedException();
    }
}
