using Fb2Library.Domain.Shared.Interfaces;

namespace Fb2Library.Infrastructure.Parsing
{
    public class DocumentParserFactory : IDocumentParserFactory
    {
        private readonly IEnumerable<IDocumentParser> _parsers;

        public DocumentParserFactory(IEnumerable<IDocumentParser> parsers)
        {
            _parsers = parsers;  // DI передает все зарегистрированные парсеры
        }

        public IDocumentParser GetDocumentParser(string fileName)
        {
            IDocumentParser parser = _parsers.FirstOrDefault(p => p.CanParse(fileName))
                ?? throw new InvalidOperationException(string.Format("{0} can't be parsed.", fileName));

            return parser;
        }
    }
}
