using Fb2Library.Domain.Persons;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Persons
{
    public class PersonTests : EntityTests<Person,  PersonId>
    {
        protected override Person CreateEntity() => Person.Create("Лев", "Толстой");
        [Fact]
        public void Create_WithValidName_ShouldSetProperties()
        {
            // Arrange
            var name = PersonName.Create("Лев", "Толстой", "Николаевич", "The Great");

            // Act
            var person = Person.Create("Лев", "Толстой", "Николаевич", "The Great");

            // Assert
            person.Name.Should().Be(name);
        }

        [Fact]
        public void Create_WithValidData_ShouldSucceed()
        {
            // Act
            var person = Person.Create("Иван", "Иванов", "Иванович");

            // Assert
            person.Name.LastName.Should().Be("Иванов");
            //person.BookAuthorships.Should().BeEmpty();
            //person.Translator.Should().BeEmpty();
            //person.DocumentAuthorships.Should().BeEmpty();
        }
    }
}
