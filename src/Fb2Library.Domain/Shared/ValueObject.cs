namespace Fb2Library.Domain.Shared
{
    public abstract record ValueObject<T>
    {
        private readonly T _value;

        protected ValueObject(T value)
        {
            if (IsNullOrEmty(value))
                throw new ArgumentException("Value cannot be empty", nameof(value));

            _value = NormalizeValue(value);
        }

        protected abstract T NormalizeValue(T code);
        protected abstract bool IsNullOrEmty(T code);
        protected abstract bool EqualsWith(ValueObject<T>? other);
        protected abstract int GetHashCodeFromValue();
        public virtual bool Equals(ValueObject<T>? other) => EqualsWith(other);
        public override int GetHashCode() => GetHashCodeFromValue();


        public T Value
        {
            get => _value;
            init
            {
                if (IsNullOrEmty(value))
                    throw new ArgumentException("Value cannot be empty");
                _value = NormalizeValue(value);
            }
        }
    }

    public abstract record StringValueObject : ValueObject<string>
    {
        protected StringValueObject(string value) : base(value) { }
        protected override bool EqualsWith(ValueObject<string>? other)
            => other is not null && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        protected override int GetHashCodeFromValue() => Value.ToUpperInvariant().GetHashCode();
        protected override bool IsNullOrEmty(string code) => string.IsNullOrWhiteSpace(code);
        protected override string NormalizeValue(string code) => code.Trim();

    }
}
