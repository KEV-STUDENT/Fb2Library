using Fb2Library.Domain.Persons;
using Fb2Library.Domain.Tests.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Persons
{
    public class PersonTests : EntityTests<Person,  PersonId, PersonName>
    {
        protected override Person CreateEntity() => Person.Create("Лев", "Толстой", "Николаевич", "The Great");
        protected override PersonName CreateValueObject() => PersonName.Create("Лев", "Толстой", "Николаевич", "The Great");

        [Fact]
        public void Create_WithValidData_ShouldSucceed()
        {
            // Act
            var person = Person.Create("Иван", "Иванов", "Иванович");

            // Assert
            person.Value.LastName.Should().Be("Иванов");
            //person.BookAuthorships.Should().BeEmpty();
            //person.Translator.Should().BeEmpty();
            //person.DocumentAuthorships.Should().BeEmpty();
        }
    }
}
