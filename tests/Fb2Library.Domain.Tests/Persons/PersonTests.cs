using Fb2Library.Domain.Persons;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Persons
{
    public class PersonTests
    {
        [Fact]
        public void Create_WithValidName_ShouldSetProperties()
        {
            // Arrange
            var name = PersonName.Create("Лев", "Толстой", "Николаевич", "The Great");

            // Act
            var person = Person.Create("Лев", "Толстой", "Николаевич", "The Great");

            // Assert
            person.Id.Value.Should().NotBeEmpty();
            person.Name.Should().Be(name);
        }

        [Fact]
        public void Create_TwoNewPerson_IdsAreNotEqual()
        {
            // Arrange & Act
            var person1 = Person.Create("Test1", "Test1");
            var person2 = Person.Create("Test2", "Test2");

            // Assert
            person1.Id.Should().NotBe(person2.Id);
        }

        [Fact]
        public void Create_WithValidData_ShouldSucceed()
        {
            // Act
            var person = Person.Create("Иван", "Иванов", "Иванович");

            // Assert
            person.Should().NotBeNull();
            person.Id.Value.Should().NotBe(Guid.Empty);
            person.Name.LastName.Should().Be("Иванов");
            //person.BookAuthorships.Should().BeEmpty();
            //person.Translator.Should().BeEmpty();
            //person.DocumentAuthorships.Should().BeEmpty();
        }
    }
}
