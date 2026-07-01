using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Books.Entities.Keywords
{
    public sealed record KeywordWord : StringValueObject
    {
        private KeywordWord(string value) : base(value){}

        public static KeywordWord Create(string code) => new(code);

        public override string ToString() => Value;
        protected override string NormalizeValue(string code) => code.Trim().ToLowerInvariant();
    }
}
