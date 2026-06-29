using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Persons
{
    public sealed class Person : AggregateRoot<PersonId, PersonName>
    {
        private Person(PersonName name) : base(name) { }
       
        public static Person Create(string firstName, string lastName, string? middleName = null, string? nickName = null)
        {
            return new Person(PersonName.Create(firstName, lastName, middleName, nickName));
        }

        protected override PersonId GetNewId() => PersonId.New();
    }
}
