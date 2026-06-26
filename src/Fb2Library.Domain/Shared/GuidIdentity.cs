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

    public abstract record GuidIdentity : Identity<Guid>, IComparable<GuidIdentity>, IComparable
    {
        protected GuidIdentity(Guid value) : base(value) { }

        protected override void Validate(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException($"{GetType().Name} Can't be empty", nameof(value));
        }

        // авто-приведение к Guid
        public static implicit operator Guid(GuidIdentity id) => id.Value;

        public int CompareTo(GuidIdentity? other)
        {
            if (other is null) return 1;
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            if (obj is GuidIdentity other) return CompareTo(other);
            throw new ArgumentException($"Object must be of type {nameof(GuidIdentity)}");
        }

        //// Операторы сравнения
        //public static bool operator <(GuidIdentity left, GuidIdentity right) => left.CompareTo(right) < 0;
        //public static bool operator >(GuidIdentity left, GuidIdentity right) => left.CompareTo(right) > 0;
        //public static bool operator <=(GuidIdentity left, GuidIdentity right) => left.CompareTo(right) <= 0;
        //public static bool operator >=(GuidIdentity left, GuidIdentity right) => left.CompareTo(right) >= 0;
    }
}
