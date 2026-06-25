namespace Fb2Library.Domain.Shared
{
    public abstract record Identity<T>
    {
        public T Value { get; }

        protected Identity(T value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            Validate(value);
            Value = value;
        }

        protected virtual void Validate(T value) { }

        public sealed override string ToString() => Value?.ToString() ?? "null";
    }

    public abstract record IntIdentity : Identity<int>, IComparable<IntIdentity>, IComparable
    {
        protected IntIdentity(int value) : base(value) { }

        protected override void Validate(int value)
        {
            if (value < 0)
                throw new ArgumentException($"{GetType().Name} must be greater than 0", nameof(value));
        }

        // авто-приведение к int
        public static implicit operator int(IntIdentity id) => id.Value;

        public int CompareTo(IntIdentity? other)
        {
            if (other is null) return 1;
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            if (obj is IntIdentity other) return CompareTo(other);
            throw new ArgumentException($"Object must be of type {nameof(IntIdentity)}");
        }

        // Операторы сравнения
        public static bool operator <(IntIdentity left, IntIdentity right) => left.CompareTo(right) < 0;
        public static bool operator >(IntIdentity left, IntIdentity right) => left.CompareTo(right) > 0;
        public static bool operator <=(IntIdentity left, IntIdentity right) => left.CompareTo(right) <= 0;
        public static bool operator >=(IntIdentity left, IntIdentity right) => left.CompareTo(right) >= 0;
    }
}
