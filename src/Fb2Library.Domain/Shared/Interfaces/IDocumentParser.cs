using Fb2Library.Domain.Books;
using Fb2Library.Domain.Shared.Enums;

namespace Fb2Library.Domain.Shared.Interfaces
{
    public interface IDocumentParser
    {
        public DocumentFormat SupportedFormat {  get; }

        public bool CanParse(string fileName);
        public Task<Book> ParseAsync(Stream fileStream, string fileName);
    }
}
