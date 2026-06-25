namespace Fb2Library.Domain.Shared
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
        where TId : IntIdentity
    {
        public TId Id { get; protected set; }

        protected Entity(TId id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        protected Entity()
        {
            Id = GetNewId();
        }

        protected abstract TId GetNewId();

        public override bool Equals(object? obj) => Equals(obj as Entity<TId>);

        public bool Equals(Entity<TId>? other)
            => other is not null && Id.Equals(other.Id);

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
            => Equals(left, right);

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
            => !Equals(left, right);
    }
}
