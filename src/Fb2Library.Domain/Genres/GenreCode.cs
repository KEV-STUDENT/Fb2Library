namespace Fb2Library.Domain.Genres
{
    public sealed record GenreCode
    {
        private readonly string _value;

        private GenreCode(string code) {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Value cannot be empty", nameof(code));

            _value = NormalizeCode(code);
        }

        private static string NormalizeCode(string code) => code.Trim().ToLowerInvariant();

        public string Value
        {
            get => _value;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Value cannot be empty");
                _value = NormalizeCode(value);
            }
        }
        public static GenreCode Create(string code) => new(code);

        public override string ToString() => Value;
    }
}
