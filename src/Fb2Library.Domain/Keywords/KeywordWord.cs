namespace Fb2Library.Domain.Keywords
{
    public sealed record KeywordWord
    {
        private readonly string _value;

        private KeywordWord(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Value cannot be empty", nameof(code));

            _value = NormalizeCode(code);
        }

        private static string NormalizeCode(string code) => code.Trim().ToLowerInvariant();

        public string Value => _value;
        public static KeywordWord Create(string code) => new(code);

        public override string ToString() => Value;
    }
}
