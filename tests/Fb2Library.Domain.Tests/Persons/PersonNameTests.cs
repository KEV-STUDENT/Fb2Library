using Fb2Library.Domain.Persons;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Persons
{
    public class PersonNameTests : ValueObjectTests<PersonName, PersonNameVO>
    {
        protected override PersonNameVO Value1 => new("Лев", "Толстой", "Николаевич", "The Great");
        protected override PersonNameVO Value2 => new("Александр", "Пушкин");
        public static IEnumerable<object[]> GetActualExpectedData()
        {
            yield return new object[] { new PersonNameVO("лев", "толстой"), new PersonNameVO("Лев", "Толстой") };
            yield return new object[] { new PersonNameVO("александр", "пушкин", "сергеевич", "the great"), new PersonNameVO("Александр", "Пушкин", "Сергеевич", "The Great") };
        }

        [Fact]
        public void Create_WithValidFirstAndLastName_ShouldSetProperties()
        {
            // Arrange & Act
            var name = PersonName.Create("Лев", "Толстой", "Николаевич", "The Great");

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
        [InlineData("Лев", "Толстой", "Николаевич", "Толстой Лев Николаевич")]
        [InlineData("лев", "толстой", "николаевич", "Толстой Лев Николаевич")]
        [InlineData("Александр", "Пушкин", "Сергеевич", "Пушкин Александр Сергеевич")]
        [InlineData("Федор", "Достоевский", null, "Достоевский Федор")]
        public void FullName_ShouldFormatCorrectly(string firstName, string lastName, string? middleName, string expected)
        {
            // Act
            var name = PersonName.Create(firstName, lastName, middleName);

            // Assert
            name.FullName.Should().Be(expected);
        }

                [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_InvalidFirstName_ShouldThrowArgumentException(string firstname)
        {
            // Act
            Func<PersonName> act = () => PersonName.Create(firstname, "Толстой");

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
            Func<PersonName> act = () => PersonName.Create("Лев", lastName);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("*lastName*");
        }

        protected override PersonName Create(PersonNameVO value) => PersonName.Create(value.FirstName, value.LastName, value.MiddleName, value.NickName);

        [Theory]
        [MemberData(nameof(GetActualExpectedData))]
        public override void Value_ShouldFormatCorrectly(PersonNameVO actual, PersonNameVO expected)
            => Value_ShouldFormatCorrectly_Exec(actual, expected);
#pragma warning disable xUnit1013
        public override void Create_InvalidWord_ShouldThrowArgumentException(PersonNameVO code) {}
#pragma warning restore xUnit1013
    }
}
