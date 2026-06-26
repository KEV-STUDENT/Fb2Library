
using System.Text.Json;
using AutoFixture;
using Fb2Library.Domain.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests
{
    public abstract class GuidIdentityTests<T> where T : GuidIdentity
    {
        protected abstract T CreateFrom(Guid value);
        protected abstract T CreateNew();
        protected abstract T ConvertFromGuid(Guid value); // for implicit operator

        #region Creating

        [Fact]
        public void From_WithValidPositiveInt_ReturnsPersonId()
        {
            // Arrange
            var validId = Guid.NewGuid();

            // Act
            T id = CreateFrom(validId);

            // Assert
            id.Value.Should().Be(validId);
        }

        [Fact]
        public void From_WithInvalidInt_ThrowsArgumentException()
        {
            // Act
            Func<T> act = () => CreateFrom(Guid.Empty);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("*Id Can't be empty*");
        }

        [Fact]
        public void New_ReturnsIdWithNotEmptyValue()
        {
            // Act
            T genreId = CreateNew();

            // Assert
            genreId.Value.Should().NotBe(Guid.Empty);
        }
        #endregion

        #region Equality
        [Fact]
        public void TwoIds_WithSameValue_AreEqual()
        {
            // Arrange
            var guid = Guid.NewGuid();
            T id1 = CreateFrom(guid);
            T id2 = CreateFrom(guid);

            // Act & Assert
            id1.Should().Be(id2);
            id1.Equals(id2).Should().BeTrue();
            (id1 == id2).Should().BeTrue();
            (id1 != id2).Should().BeFalse();
        }

        [Fact]
        public void TwoIds_WithDifferentValues_AreNotEqual()
        {
            // Arrange
            T id1 = CreateFrom(Guid.NewGuid());
            T id2 = CreateFrom(Guid.NewGuid());

            // Act & Assert
            id1.Should().NotBe(id2);
            id1.Equals(id2).Should().BeFalse();
            (id1 == id2).Should().BeFalse();
            (id1 != id2).Should().BeTrue();
        }

        [Fact]
        public void Id_WithNull_IsNotEqual()
        {
            // Arrange
            T id = CreateFrom(Guid.NewGuid());

            // Act & Assert
            id.Equals(null).Should().BeFalse();
            (id == null!).Should().BeFalse();
        }

        [Fact]
        public void Id_WithDifferentType_IsNotEqual()
        {
            // Arrange
            T id = CreateFrom(Guid.NewGuid());
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
            T id1 = CreateFrom(guid);
            T id2 = CreateFrom(guid);

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
            T id1 = CreateFrom(Guid.NewGuid());
            T id2 = CreateFrom(Guid.NewGuid());

            // Act
            var hash1 = id1.GetHashCode();
            var hash2 = id2.GetHashCode();

            // Assert
            hash1.Should().NotBe(hash2);
        }

        #endregion

        #region Использование в коллекциях

        [Fact]
        public void Id_CanBeUsedAsDictionaryKey()
        {
            // Arrange
            var dict = new Dictionary<T, string>();
            var guid = Guid.NewGuid();
            T id1 = CreateFrom(guid);
            T id2 = CreateFrom(guid); // То же значение

            // Act
            dict[id1] = "Test";

            // Assert
            dict.ContainsKey(id2).Should().BeTrue();
            dict[id2].Should().Be("Test");
        }

        [Fact]
        public void Id_CanBeUsedInHashSet()
        {
            // Arrange
            var set = new HashSet<T>();
            var guid = Guid.NewGuid();
            T id1 = CreateFrom(guid);
            T id2 = CreateFrom(guid);

            // Act
            set.Add(id1);
            set.Add(id2);

            // Assert
            set.Should().HaveCount(1);
            set.Contains(id1).Should().BeTrue();
        }

        [Fact]
        public void Id_CanBeUsedInList()
        {
            // Arrange
            var ids = new List<T>();
            var guid = Guid.NewGuid();
            T id1 = CreateFrom(guid);
            T id2 = CreateFrom(guid);

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
        public void ImplicitConversion_FromIntToId_Works()
        {
            // Arrange
            var value = Guid.NewGuid();

            // Act
            T id = ConvertFromGuid(value);

            // Assert
            id.Value.Should().Be(value);
        }

        [Fact]
        public void ImplicitConversion_FromIdToInt_Works()
        {
            // Arrange
            var guid = Guid.NewGuid();
            T id = CreateFrom(guid);

            // Act
            Guid value = id;

            // Assert
            value.Should().Be(guid);
        }

        [Fact]
        public void ImplicitConversion_WithInvalidValue_ThrowsException()
        {
            // Act
            Action act = () => { T id = ConvertFromGuid(Guid.Empty); };

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
            T id = CreateFrom(guid);

            // Act
            var result = id.ToString();

            // Assert
            result.Should().Be(guid.ToString());
        }

        [Fact]
        public void ToString_ForNewId_ReturnsNotEmpty()
        {
            // Arrange
            T id = CreateNew();

            // Act
            var result = id.ToString();

            // Assert
            result.Should().NotBeEmpty();
        }

        #endregion

        #region Сериализация (JSON)

        [Fact]
        public void Id_CanBeSerializedToJson()
        {
            // Arrange
            var guid = Guid.NewGuid();
            T id = CreateFrom(guid);

            // Act
            var json = JsonSerializer.Serialize(id);
            T? deserialized = JsonSerializer.Deserialize<T>(json);

            // Assert
            deserialized.Should().Be(id);
            deserialized?.Value.Should().Be(guid);
        }

        [Fact]
        public void PersonId_CanBeSerializedAsProperty()
        {
            // Arrange
            var obj = new TestClass { Id = CreateFrom(Guid.NewGuid()), Name = "Test" };

            // Act
            var json = JsonSerializer.Serialize(obj);
            TestClass? deserialized = JsonSerializer.Deserialize<TestClass>(json);

            // Assert
            deserialized?.Id.Should().Be(obj.Id);
            deserialized?.Name.Should().Be("Test");
        }

        private class TestClass
        {
            public T? Id { get; set; }
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
            var ids = generator.Take(100).Select(CreateFrom).ToList();

            foreach (T? id in ids)
            {
                id.Value.Should().NotBeEmpty();
            }
        }
        #endregion

    }
}
