using Fb2Library.Domain.Exceptions;
using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Books.Entities.Keywords
{
    public class Keyword : AggregateRoot<KeywordId, KeywordWord>
    {
        private Keyword(KeywordWord word) : base(word) { }
        public static Keyword Create(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) throw new DomainException("Keyword must be specified");
            return new(KeywordWord.Create(word));
        }
        protected override KeywordId GetNewId() => KeywordId.New();
    }
}
