namespace Fb2Library.Domain.Sequences
{
    public sealed record SequenceName
    {
        private readonly string _value;

        private SequenceName(string value)
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
        public static SequenceName Create(string code) => new(code);

        public override string ToString() => Value;

        public bool Equals(SequenceName? other) =>
            other is not null && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

        public override int GetHashCode() => Value.ToUpperInvariant().GetHashCode();
    }
}
