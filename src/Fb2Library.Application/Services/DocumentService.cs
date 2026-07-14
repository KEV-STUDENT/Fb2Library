using Fb2Library.Application.DTOs;
using Fb2Library.Application.Interfaces;

namespace Fb2Library.Application.Services
{
    public class DocumentService : IDocumentService
    {
        public Task<DocumentInfoDto> GetDocumentInfoAsync(Stream stream, string fileName) => throw new NotImplementedException();
        public Task<DocumentDto> ParseDocumentAsync(Stream stream, string fileName) => throw new NotImplementedException();
    }
}
