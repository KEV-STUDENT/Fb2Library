using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Genres
{
    public sealed record GenreCode : StringValueObject
    {
        private GenreCode(string value) : base(value){}
        protected override string NormalizeValue(string code) => code.Trim().ToLowerInvariant();
        public static GenreCode Create(string code) => new(code);
        public override string ToString() => Value;
    }
}
