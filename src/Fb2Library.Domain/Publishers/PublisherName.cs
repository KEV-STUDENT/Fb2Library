namespace Fb2Library.Domain.Publishers
{
    public sealed record PublisherName
    {
        private readonly string _value;

        private PublisherName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be empty", nameof(value));

            _value = NormalizeCode(value);
        }

        private static string NormalizeCode(string code) => code.Trim();

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
        public static PublisherName Create(string code) => new(code);

        public override string ToString() => Value;

        public bool Equals(PublisherName? other) =>
            other is not null && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

        public override int GetHashCode() => Value.ToUpperInvariant().GetHashCode();
    }
}
