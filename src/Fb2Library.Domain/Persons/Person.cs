namespace Fb2Library.Domain.Persons
{
    public class Person
    {
        public Person(PersonName name)
        {
            Name = name;
            Id = PersonId.New();
        }

        public PersonName Name { get; }
        public PersonId Id { get; set; }
    }
}
