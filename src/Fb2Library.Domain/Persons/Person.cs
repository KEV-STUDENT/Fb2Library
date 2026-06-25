using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Persons
{
    public sealed class Person : AggregateRoot<PersonId>
    {
        public Person(PersonName name)
        {
            Name = name;
            Id = PersonId.New();
        }

        public PersonName Name { get; }

        protected override PersonId GetNewId() => PersonId.New();
    }
}
