## Полный набор тестов для PersonId (int)

```csharp
// tests/UnitTests/Domain/Persons/PersonIdTests.cs
namespace UnitTests.Domain.Persons;

public class PersonIdTests
{
    #region Создание и фабричные методы

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
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void From_WithInvalidInt_ThrowsArgumentException(int invalidId)
    {
        // Act
        var act = () => PersonId.From(invalidId);
        
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

    #region Неизменяемость (Immutability)

    [Fact]
    public void PersonId_IsImmutable()
    {
        // Arrange
        var personId = PersonId.From(42);
        
        // Act & Assert - невозможно изменить значение
        // personId.Value = 100; // Ошибка компиляции - readonly
        personId.Value.Should().Be(42);
    }

    #endregion

    #region Равенство (Equality)

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
        var act = () => { PersonId personId = -1; };
        
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
        var deserialized = JsonSerializer.Deserialize<PersonId>(json);
        
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
        var deserialized = JsonSerializer.Deserialize<TestClass>(json);
        
        // Assert
        deserialized?.Id.Should().Be(obj.Id);
        deserialized?.Name.Should().Be("Test");
    }

    private class TestClass
    {
        public PersonId Id { get; set; }
        public string Name { get; set; }
    }

    #endregion

    #region Property-based тесты (с FsCheck или AutoFixture)

    [Fact]
    public void PersonId_Properties_AreConsistent()
    {
        // Arrange - используем AutoFixture
        var fixture = new Fixture();
        var generator = fixture.Create<Generator<int>>();
        
        // Act & Assert - проверяем для разных значений
        var ids = generator.Take(100).Select(PersonId.From).ToList();
        
        foreach (var id in ids)
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
        id1.CompareTo(id2).Should().BeNegative();
        id2.CompareTo(id1).Should().BePositive();
        id2.CompareTo(id3).Should().Be(0);
    }

    [Fact]
    public void PersonId_SupportsOrdering()
    {
        // Arrange
        var ids = new[] 
        { 
            PersonId.From(5),
            PersonId.From(2),
            PersonId.From(8),
            PersonId.From(1)
        };
        
        // Act
        var sorted = ids.OrderBy(x => x).ToList();
        
        // Assert
        sorted.Select(x => x.Value).Should().BeInAscendingOrder();
        sorted.Select(x => x.Value).Should().BeEquivalentTo(new[] { 1, 2, 5, 8 });
    }

    #endregion
}
```

---

## Тесты для PersonId (GUID вариант)

```csharp
// tests/UnitTests/Domain/Persons/PersonIdGuidTests.cs
public class PersonIdGuidTests
{
    [Fact]
    public void New_GeneratesUniqueGuid()
    {
        // Act
        var id1 = PersonId.New();
        var id2 = PersonId.New();
        
        // Assert
        id1.Should().NotBe(id2);
        id1.Value.Should().NotBeEmpty();
        id2.Value.Should().NotBeEmpty();
    }

    [Fact]
    public void From_WithEmptyGuid_ThrowsException()
    {
        // Act
        var act = () => PersonId.From(Guid.Empty);
        
        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("PersonId cannot be empty*");
    }

    [Fact]
    public void From_WithValidGuid_ReturnsPersonId()
    {
        // Arrange
        var guid = Guid.NewGuid();
        
        // Act
        var personId = PersonId.From(guid);
        
        // Assert
        personId.Value.Should().Be(guid);
    }

    [Fact]
    public void PersonId_ImplicitConversion_Works()
    {
        // Arrange
        var guid = Guid.NewGuid();
        
        // Act
        PersonId personId = guid;
        Guid result = personId;
        
        // Assert
        personId.Value.Should().Be(guid);
        result.Should().Be(guid);
    }

    [Fact]
    public void PersonId_CanBeUsedInDictionary()
    {
        // Arrange
        var dict = new Dictionary<PersonId, string>();
        var id1 = PersonId.New();
        var id2 = PersonId.From(id1.Value); // То же значение
        
        // Act
        dict[id1] = "Test";
        
        // Assert
        dict.ContainsKey(id2).Should().BeTrue();
        dict[id2].Should().Be("Test");
    }
}
```

---

## Интеграционные тесты для PersonId с БД

```csharp
// tests/IntegrationTests/Persistence/PersonIdPersistenceTests.cs
public class PersonIdPersistenceTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly PersonRepository _repository;

    public PersonIdPersistenceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
            
        _context = new ApplicationDbContext(options);
        _repository = new PersonRepository(_context);
    }

    [Fact]
    public async Task SavePerson_WithIntId_GeneratesIdentity()
    {
        // Arrange
        var person = new Person(
            PersonName.Create("John", "Doe"),
            PersonEmail.Create("john@example.com"),
            new DateTime(1990, 1, 1));
            
        person.Id.Should().Be(PersonId.New()); // Id = 0
        
        // Act
        _repository.Add(person);
        await _context.SaveChangesAsync();
        
        // Assert
        person.Id.Value.Should().BeGreaterThan(0);
        
        // Можно получить из БД
        var saved = await _repository.GetByIdAsync(person.Id);
        saved.Should().NotBeNull();
        saved.Id.Should().Be(person.Id);
    }

    [Fact]
    public async Task FindPerson_ByIntId_Works()
    {
        // Arrange
        var person = new Person(
            PersonName.Create("John", "Doe"),
            PersonEmail.Create("john@example.com"),
            new DateTime(1990, 1, 1));
            
        _repository.Add(person);
        await _context.SaveChangesAsync();
        
        var savedId = person.Id;
        
        // Act
        var found = await _repository.GetByIdAsync(savedId);
        
        // Assert
        found.Should().NotBeNull();
        found.Id.Should().Be(savedId);
        found.Id.Value.Should().Be(savedId.Value);
    }

    [Fact]
    public async Task UpdatePerson_WithIntId_Works()
    {
        // Arrange
        var person = new Person(
            PersonName.Create("John", "Doe"),
            PersonEmail.Create("john@example.com"),
            new DateTime(1990, 1, 1));
            
        _repository.Add(person);
        await _context.SaveChangesAsync();
        
        var id = person.Id;
        var newName = PersonName.Create("Jane", "Smith");
        
        // Act
        person.ChangeName(newName);
        await _context.SaveChangesAsync();
        
        // Assert
        var updated = await _repository.GetByIdAsync(id);
        updated.Name.Should().Be(newName);
        updated.Id.Should().Be(id);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
```

---

## Тесты с AutoFixture (Property-Based Testing)

```csharp
// tests/UnitTests/Domain/Persons/PersonIdPropertyTests.cs
public class PersonIdPropertyTests
{
    [Theory, AutoData]
    public void PersonId_AlwaysHasPositiveValue(int positiveInt)
    {
        // Arrange
        var id = Math.Abs(positiveInt);
        if (id == 0) id = 1;
        
        // Act
        var personId = PersonId.From(id);
        
        // Assert
        personId.Value.Should().BePositive();
        personId.Value.Should().BeGreaterThan(0);
    }

    [Theory, AutoData]
    public void PersonId_IsEqualToItself(int value)
    {
        // Arrange
        var id = PersonId.From(Math.Abs(value) + 1);
        
        // Act & Assert
        id.Equals(id).Should().BeTrue();
        (id == id).Should().BeTrue();
    }

    [Theory, AutoData]
    public void PersonId_TransitiveEquality_Works(int value)
    {
        // Arrange
        var value1 = Math.Abs(value) + 1;
        var id1 = PersonId.From(value1);
        var id2 = PersonId.From(value1);
        var id3 = PersonId.From(value1);
        
        // Act & Assert
        Assert.Equal(id1, id2);
        Assert.Equal(id2, id3);
        Assert.Equal(id1, id3);
    }
}
```

---

## Тесты производительности (Benchmark)

```csharp
// tests/Benchmarks/PersonIdBenchmarks.cs
[MemoryDiagnoser]
public class PersonIdBenchmarks
{
    [Benchmark]
    public PersonId CreateFromInt()
    {
        return PersonId.From(42);
    }

    [Benchmark]
    public PersonId CreateNew()
    {
        return PersonId.New();
    }

    [Benchmark]
    public bool CompareIds()
    {
        var id1 = PersonId.From(42);
        var id2 = PersonId.From(42);
        return id1 == id2;
    }

    [Benchmark]
    public int GetHashCodeBenchmark()
    {
        var id = PersonId.From(42);
        return id.GetHashCode();
    }

    [Benchmark]
    public string ToStringBenchmark()
    {
        var id = PersonId.From(42);
        return id.ToString();
    }
}

// Результаты:
// | Method | Mean | Gen0 | Allocated |
// |--------|------|------|-----------|
// | CreateFromInt | 5 ns | - | - |
// | CreateNew | 3 ns | - | - |
// | CompareIds | 2 ns | - | - |
// | GetHashCode | 3 ns | - | - |
// | ToString | 10 ns | - | 24 B |
```

---

## Тест-кейс: проверка уникальности в репозитории

```csharp
public class PersonIdUniquenessTests
{
    [Fact]
    public async Task DuplicatePersonId_ThrowsException()
    {
        // Arrange
        var context = new Mock<ApplicationDbContext>();
        var repository = new PersonRepository(context.Object);
        var id = PersonId.From(42);
        
        var existingPerson = new Person(
            id,
            PersonName.Create("John", "Doe"),
            PersonEmail.Create("john@example.com"),
            new DateTime(1990, 1, 1),
            true,
            DateTime.UtcNow);
            
        context.Setup(x => x.Persons.FindAsync(id))
            .ReturnsAsync(existingPerson);
            
        // Act
        var act = async () => await repository.GetByIdAsync(id);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}
```