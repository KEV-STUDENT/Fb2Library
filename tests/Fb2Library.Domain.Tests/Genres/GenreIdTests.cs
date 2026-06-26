
using System.Text.Json;
using AutoFixture;
using Fb2Library.Domain.Genres;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Genres
{
    public class GenreIdTests
    {
        #region Creating

        [Fact]
        public void From_WithValidPositiveInt_ReturnsPersonId()
        {
            // Arrange
            var validId = Guid.NewGuid();

            // Act
            var genreId = GenreId.From(validId);

            // Assert
            genreId.Value.Should().Be(validId);
        }

        [Fact]
        public void From_WithInvalidInt_ThrowsArgumentException()
        {
            // Act
            Func<GenreId> act = () => GenreId.From(Guid.Empty);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("GenreId Can't be empty*");
        }

        [Fact]
        public void New_ReturnsPersonIdWithZeroValue()
        {
            // Act
            var genreId = GenreId.New();

            // Assert
            genreId.Value.Should().NotBe(Guid.Empty);
        }
        #endregion
            
        #region Equality
        [Fact]
        public void TwoPersonIds_WithSameValue_AreEqual()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var id1 = GenreId.From(guid);
            var id2 = GenreId.From(guid);

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
            var id1 = GenreId.From(Guid.NewGuid());
            var id2 = GenreId.From(Guid.NewGuid());

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
            var id = GenreId.From(Guid.NewGuid());

            // Act & Assert
            id.Equals(null).Should().BeFalse();
            (id == null!).Should().BeFalse();
        }

        [Fact]
        public void PersonId_WithDifferentType_IsNotEqual()
        {
            // Arrange
            var id = GenreId.From(Guid.NewGuid());
            var obj = new object();

            // Act & Assert
            id.Equals(obj).Should().BeFalse();
        }

        #endregion

        #region GetHashCode

        [Fact]
        public void GetHashCode_ForSameValue_ReturnsSameHashCode()
        {
            var guid = Guid.NewGuid();
            // Arrange
            var id1 = GenreId.From(guid);
            var id2 = GenreId.From(guid);

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
            var id1 = GenreId.From(Guid.NewGuid());
            var id2 = GenreId.From(Guid.NewGuid());

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
            var dict = new Dictionary<GenreId, string>();
            var guid = Guid.NewGuid();
            var id1 = GenreId.From(guid);
            var id2 = GenreId.From(guid); // То же значение

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
            var set = new HashSet<GenreId>();
            var guid = Guid.NewGuid();
            var id1 = GenreId.From(guid);
            var id2 = GenreId.From(guid);

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
            var ids = new List<GenreId>();
            var guid = Guid.NewGuid();
            var id1 = GenreId.From(guid);
            var id2 = GenreId.From(guid);

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
            var value = Guid.NewGuid();

            // Act
            GenreId id = value;

            // Assert
            id.Value.Should().Be(value);
        }

        [Fact]
        public void ImplicitConversion_FromPersonIdToInt_Works()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var id = GenreId.From(guid);

            // Act
            Guid value = id;

            // Assert
            value.Should().Be(guid);
        }

        [Fact]
        public void ImplicitConversion_WithInvalidValue_ThrowsException()
        {
            // Act
            Action act = () => { GenreId id = Guid.Empty; };

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        #endregion

        #region ToString

        [Fact]
        public void ToString_ReturnsValueAsString()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var id = GenreId.From(guid);

            // Act
            var result = id.ToString();

            // Assert
            result.Should().Be(guid.ToString());
        }

        [Fact]
        public void ToString_ForNewId_ReturnsNotEmpty()
        {
            // Arrange
            var id = GenreId.New();

            // Act
            var result = id.ToString();

            // Assert
            result.Should().NotBeEmpty();
        }

        #endregion

        #region Сериализация (JSON)

        [Fact]
        public void PersonId_CanBeSerializedToJson()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var id = GenreId.From(guid);

            // Act
            var json = JsonSerializer.Serialize(id);
            GenreId? deserialized = JsonSerializer.Deserialize<GenreId>(json);

            // Assert
            deserialized.Should().Be(id);
            deserialized?.Value.Should().Be(guid);
        }

        [Fact]
        public void PersonId_CanBeSerializedAsProperty()
        {
            // Arrange
            var obj = new TestClass { Id = GenreId.From(Guid.NewGuid()), Name = "Test" };

            // Act
            var json = JsonSerializer.Serialize(obj);
            TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json);

            // Assert
            deserialized?.Id.Should().Be(obj.Id);
            deserialized?.Name.Should().Be("Test");
        }

        private class TestClass
        {
            public GenreId? Id { get; set; }
            public string? Name { get; set; }
        }

        #endregion

        #region Property-based тесты (с FsCheck или AutoFixture)

        [Fact]
        public void PersonId_Properties_AreConsistent()
        {
            // Arrange - используем AutoFixture
            var fixture = new Fixture();
            Generator<Guid> generator = fixture.Create<Generator<Guid>>();

            // Act & Assert - проверяем для разных значений
            var ids = generator.Take(100).Select(GenreId.From).ToList();

            foreach (GenreId? id in ids)
            {
                id.Value.Should().NotBeEmpty();
            }
        }
        #endregion
    }
}

