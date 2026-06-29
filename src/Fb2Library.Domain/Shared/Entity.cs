namespace Fb2Library.Domain.Shared
{
    public abstract class Entity<TId, TValueObject> : IEquatable<Entity<TId, TValueObject>>
        where TId : GuidIdentity
    {
        public TId Id { get; protected set; }
        public TValueObject Value { get; private set; }

        protected Entity(TId id, TValueObject valueObject)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Value = valueObject ?? throw new ArgumentNullException(nameof(valueObject)); 
        }

        protected Entity(TValueObject valueObject) {
            Id = GetNewId();
            Value = valueObject ?? throw new ArgumentNullException(nameof(valueObject));
        }

        protected  abstract TId GetNewId();

        public override bool Equals(object? obj) => Equals(obj as Entity<TId, TValueObject>);


        public bool Equals(Entity<TId, TValueObject>? other)
            => other is not null && Id.Equals(other.Id);

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Entity<TId, TValueObject> left, Entity<TId, TValueObject> right)
            => Equals(left, right);

        public static bool operator !=(Entity<TId, TValueObject> left, Entity<TId, TValueObject> right)
            => !Equals(left, right);
    }
}
