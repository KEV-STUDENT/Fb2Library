using Fb2Library.Domain.Persons;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Persons
{
    public class PersonNameTests
    {
        [Fact]
        public void Create_WithValidFirstAndLastName_ShouldSetProperties()
        {
            // Arrange & Act
            var name = new PersonName("Лев", "Толстой", "Николаевич", "The Great");

            // Assert
            name.FirstName.Should().Be("Лев");
            name.LastName.Should().Be("Толстой");
            name.MiddleName.Should().Be("Николаевич");
            name.NickName.Should().Be("The Great");
            name.ShortName.Should().Be("Лев Толстой");
            name.SortName.Should().Be("Толстой Лев");
            name.FullName.Should().Be("Толстой Лев Николаевич");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidFirstName_ShouldThrowArgumentException(string firstname)
        {
            // Act
            Func<PersonName> act = () => new PersonName(firstname, "Толстой");

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("*firstName*");
        }


        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidLastName_ShouldThrowArgumentException(string lastName)
        {
            // Act
            Func<PersonName> act = () => new PersonName("Лев", lastName);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("*lastName*");
        }

        [Theory]
        [InlineData("Лев", "Толстой", "Николаевич", "Толстой Лев Николаевич")]
        [InlineData("лев", "толстой", "николаевич", "Толстой Лев Николаевич")]
        [InlineData("Александр", "Пушкин", "Сергеевич", "Пушкин Александр Сергеевич")]
        [InlineData("Федор", "Достоевский", null, "Достоевский Федор")]
        public void FullName_ShouldFormatCorrectly(string firstName, string lastName, string? middleName, string expected)
        {
            // Act
            var name = new PersonName(firstName, lastName, middleName);

            // Assert
            name.FullName.Should().Be(expected);
        }

        [Fact]
        public void TwoInstances_WithSameValues_ShouldBeEqual()
        {
            // Arrange
            var name1 = new PersonName("Лев", "Толстой");
            var name2 = new PersonName("Лев", "Толстой");

            // Assert
            name1.Should().Be(name2);
            name1.GetHashCode().Should().Be(name2.GetHashCode());
        }

        [Fact]
        public void TwoInstances_WithDifferentValues_ShouldNotBeEqual()
        {
            // Arrange
            var name1 = new PersonName("Лев", "Толстой");
            var name2 = new PersonName("Александр", "Пушкин");

            // Assert
            name1.Should().NotBe(name2);
        }
    }
}
