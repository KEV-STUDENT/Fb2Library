namespace Fb2Library.Domain.Shared.Interfaces
{
    public interface IDocumentParserFactory
    {
        public IDocumentParser GetDocumentParser(string fileName);
    }
}
