using Fb2Library.Domain.Persons;
using FluentAssertions;
using System.Text.Json;
using AutoFixture;

namespace Fb2Library.Domain.Tests.Persons
{
    public class PersonIdTests
    {
        #region Creating

        [Fact]
        public void From_WithValidPositiveInt_ReturnsPersonId()
        {
            // Arrange
            const int validId = 42;

            // Act
            var personId = PersonId.From(validId);

            // Assert
            personId.Value.Should().Be(validId);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void From_WithInvalidInt_ThrowsArgumentException(int invalidId)
        {
            // Act
            Func<PersonId> act = () => PersonId.From(invalidId);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("PersonId must be greater than 0*");
        }

        [Fact]
        public void New_ReturnsPersonIdWithZeroValue()
        {
            // Act
            var personId = PersonId.New();

            // Assert
            personId.Value.Should().Be(0);
        }
        #endregion


        #region Equality
        [Fact]
        public void TwoPersonIds_WithSameValue_AreEqual()
        {
            // Arrange
            var id1 = PersonId.From(42);
            var id2 = PersonId.From(42);

            // Act & Assert
            id1.Should().Be(id2);
            id1.Equals(id2).Should().BeTrue();
            (id1 == id2).Should().BeTrue();
            (id1 != id2).Should().BeFalse();
        }

        [Fact]
        public void TwoPersonIds_WithDifferentValues_AreNotEqual()
        {
            // Arrange
            var id1 = PersonId.From(42);
            var id2 = PersonId.From(100);

            // Act & Assert
            id1.Should().NotBe(id2);
            id1.Equals(id2).Should().BeFalse();
            (id1 == id2).Should().BeFalse();
            (id1 != id2).Should().BeTrue();
        }

        [Fact]
        public void PersonId_WithNull_IsNotEqual()
        {
            // Arrange
            var id = PersonId.From(42);

            // Act & Assert
            id.Equals(null).Should().BeFalse();
            (id == null!).Should().BeFalse();
        }

        [Fact]
        public void PersonId_WithDifferentType_IsNotEqual()
        {
            // Arrange
            var id = PersonId.From(42);
            var obj = new object();

            // Act & Assert
            id.Equals(obj).Should().BeFalse();
        }

        #endregion

        #region GetHashCode

        [Fact]
        public void GetHashCode_ForSameValue_ReturnsSameHashCode()
        {
            // Arrange
            var id1 = PersonId.From(42);
            var id2 = PersonId.From(42);

            // Act
            var hash1 = id1.GetHashCode();
            var hash2 = id2.GetHashCode();

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public void GetHashCode_ForDifferentValues_ReturnsDifferentHashCodes()
        {
            // Arrange
            var id1 = PersonId.From(42);
            var id2 = PersonId.From(100);

            // Act
            var hash1 = id1.GetHashCode();
            var hash2 = id2.GetHashCode();

            // Assert
            hash1.Should().NotBe(hash2);
        }

        #endregion

        #region Использование в коллекциях

        [Fact]
        public void PersonId_CanBeUsedAsDictionaryKey()
        {
            // Arrange
            var dict = new Dictionary<PersonId, string>();
            var id1 = PersonId.From(42);
            var id2 = PersonId.From(42); // То же значение

            // Act
            dict[id1] = "Test";

            // Assert
            dict.ContainsKey(id2).Should().BeTrue();
            dict[id2].Should().Be("Test");
        }

        [Fact]
        public void PersonId_CanBeUsedInHashSet()
        {
            // Arrange
            var set = new HashSet<PersonId>();
            var id1 = PersonId.From(42);
            var id2 = PersonId.From(42);

            // Act
            set.Add(id1);
            set.Add(id2);

            // Assert
            set.Should().HaveCount(1);
            set.Contains(id1).Should().BeTrue();
        }

        [Fact]
        public void PersonId_CanBeUsedInList()
        {
            // Arrange
            var ids = new List<PersonId>();
            var id1 = PersonId.From(42);
            var id2 = PersonId.From(42);

            // Act
            ids.Add(id1);
            ids.Add(id2);

            // Assert
            ids.Should().HaveCount(2);
            ids.Should().Contain(id1);
            ids.Should().Contain(id2);
        }

        #endregion

        #region Операторы приведения

        [Fact]
        public void ImplicitConversion_FromIntToPersonId_Works()
        {
            // Arrange
            const int value = 42;

            // Act
            PersonId personId = value;

            // Assert
            personId.Value.Should().Be(value);
        }

        [Fact]
        public void ImplicitConversion_FromPersonIdToInt_Works()
        {
            // Arrange
            var personId = PersonId.From(42);

            // Act
            int value = personId;

            // Assert
            value.Should().Be(42);
        }

        [Fact]
        public void ImplicitConversion_WithInvalidValue_ThrowsException()
        {
            // Act
            Action act = () => { PersonId personId = -1; };

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        #endregion

        #region ToString

        [Fact]
        public void ToString_ReturnsValueAsString()
        {
            // Arrange
            var personId = PersonId.From(42);

            // Act
            var result = personId.ToString();

            // Assert
            result.Should().Be("42");
        }

        [Fact]
        public void ToString_ForNewId_ReturnsZero()
        {
            // Arrange
            var personId = PersonId.New();

            // Act
            var result = personId.ToString();

            // Assert
            result.Should().Be("0");
        }

        #endregion

        #region Сериализация (JSON)

        [Fact]
        public void PersonId_CanBeSerializedToJson()
        {
            // Arrange
            var personId = PersonId.From(42);

            // Act
            var json = JsonSerializer.Serialize(personId);
            PersonId? deserialized = JsonSerializer.Deserialize<PersonId>(json);

            // Assert
            deserialized.Should().Be(personId);
            deserialized?.Value.Should().Be(42);
        }

        [Fact]
        public void PersonId_CanBeSerializedAsProperty()
        {
            // Arrange
            var obj = new TestClass { Id = PersonId.From(42), Name = "Test" };

            // Act
            var json = JsonSerializer.Serialize(obj);
            TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json);

            // Assert
            deserialized?.Id.Should().Be(obj.Id);
            deserialized?.Name.Should().Be("Test");
        }

        private class TestClass
        {
            public PersonId? Id { get; set; }
            public string? Name { get; set; }
        }

        #endregion

        #region Property-based тесты (с FsCheck или AutoFixture)

        [Fact]
        public void PersonId_Properties_AreConsistent()
        {
            // Arrange - используем AutoFixture
            var fixture = new Fixture();
            Generator<int> generator = fixture.Create<Generator<int>>();

            // Act & Assert - проверяем для разных значений
            var ids = generator.Take(100).Select(PersonId.From).ToList();

            foreach (PersonId? id in ids)
            {
                id.Value.Should().BeGreaterThan(0);
            }
        }

        #endregion

        #region Граничные случаи

        [Fact]
        public void PersonId_WithMaxIntValue_Works()
        {
            // Arrange
            const int maxValue = int.MaxValue;

            // Act
            var personId = PersonId.From(maxValue);

            // Assert
            personId.Value.Should().Be(maxValue);
        }

        [Fact]
        public void PersonId_WithMinPositiveValue_Works()
        {
            // Arrange
            const int minValue = 1;

            // Act
            var personId = PersonId.From(minValue);

            // Assert
            personId.Value.Should().Be(minValue);
        }

        #endregion

        #region Сравнение

        [Fact]
        public void PersonId_ImplementsIComparable()
        {
            // Arrange
            var id1 = PersonId.From(1);
            var id2 = PersonId.From(2);
            var id3 = PersonId.From(2);

            // Act & Assert
            id1.Should().BeLessThan(id2);

            id1.CompareTo(id2).Should().BeNegative();
            id2.CompareTo(id1).Should().BePositive();
            id2.CompareTo(id3).Should().Be(0);
        }

        [Fact]
        public void PersonId_SupportsOrdering()
        {
            // Arrange
            PersonId[] ids = [
                PersonId.From(5),
                PersonId.From(2),
                PersonId.From(8),
                PersonId.From(1)
                ];

            // Act
            var sorted = ids.OrderBy(x => x).ToList();

            // Assert
            sorted.Select(x => x.Value).Should().BeInAscendingOrder();
            sorted.Select(x => x.Value).Should().BeEquivalentTo(new[] { 1, 2, 5, 8 });
        }

        #endregion
    }
}
