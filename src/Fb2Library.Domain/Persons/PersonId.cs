using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Persons
{
    public record PersonId : IntIdentity
    {
        public PersonId(int value) : base(value) { }

        public static PersonId From(int value) => new(value);
        public static PersonId New() => new(0);

        public static implicit operator PersonId(int value) => From(value);
    }
}
