using Fb2Library.Domain.Shared;
using Fb2Library.Domain.Exceptions;

namespace Fb2Library.Domain.Persons
{
    public sealed class Person : AggregateRoot<PersonId>
    {
        private Person(PersonName name)
        {
            if (name is null)
                throw new DomainException("Name for Person must be specify");

            Name = name;
            Id = PersonId.New();
        }

        public PersonName Name { get; }

        public static Person Create(string firstName, string lastName, string? middleName = null, string? nickName = null)
        {
            return new Person(PersonName.Create(firstName, lastName, middleName, nickName));
        }

        protected override PersonId GetNewId() => PersonId.New();
    }
}
