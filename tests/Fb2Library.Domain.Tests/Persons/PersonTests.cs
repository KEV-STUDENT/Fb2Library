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
            var name = new PersonName("Лев", "Толстой", "Николаевич", "The Great");

            // Act
            var person = new Person(name);

            // Assert
            person.Id.Value.Should().NotBeEmpty();
            person.Name.Should().Be(name);
        }
    }
}
