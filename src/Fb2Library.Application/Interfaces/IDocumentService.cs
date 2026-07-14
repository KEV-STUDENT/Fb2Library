using Fb2Library.Application.DTOs;

namespace Fb2Library.Application.Interfaces
{
    public interface IDocumentService
    {
        public Task<DocumentDto> ParseDocumentAsync(Stream stream, string fileName);
        public Task<DocumentInfoDto> GetDocumentInfoAsync(Stream stream, string fileName);
    }
}
