using Fb2Library.Domain.Books;
using Fb2Library.Domain.Shared.Enums;
using Fb2Library.Domain.Shared.Interfaces;

namespace Fb2Library.Domain.Shared
{
    public abstract class DocumentParser : IDocumentParser
    {
        public abstract DocumentFormat SupportedFormat{ get; }
        public bool CanParse(string fileName)
        {
            return SupportedFormat.ToString().Equals(Path.GetExtension(fileName).TrimStart('.'), StringComparison.OrdinalIgnoreCase);
        }
        public abstract Task<Book> ParseAsync(Stream fileStream, string fileName);
    }
}
