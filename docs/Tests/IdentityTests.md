
Identity - Id для сушности, ValueObject и могут быть организованы на основе int или GUID

## Почему тесты для Identity критичны?

1. **Value Object - основа идентификации** - ошибка здесь разрушит всю систему
2. **Инварианты** - должны быть гарантированы всегда
3. **Equals/GetHashCode** - критичны для работы коллекций, словарей, EF Core
4. **Конвертации** - работа с БД, API, сериализация

---

## Что должно быть в тестах для PersonId (чеклист)

| Тест-кейс | Важность | Описание |
|-----------|----------|----------|
| ✅ Создание с валидным значением | **Critical** | Проверка успешного создания |
| ✅ Создание с невалидным значением | **Critical** | Проверка валидации |
| ✅ Сравнение на равенство | **Critical** | Equals, ==, != |
| ✅ GetHashCode | **Critical** | Для Dictionary/HashSet |
| ✅ Неизменяемость (Immutability) | **High** | Проверка readonly |
| ✅ Использование в коллекциях | **High** | Dictionary, List, HashSet |
| ✅ Сериализация JSON | **High** | Для API |
| ✅ Преобразования (implicit/explicit) | **Medium** | Работа с БД, маппинг |
| ✅ ToString | **Medium** | Логирование, отладка |
| ✅ Сравнение (IComparable) | **Medium** | Сортировка |
| ✅ Граничные случаи | **Medium** | MaxInt, MinInt, 0, -1 |
| ✅ Property-based тесты | **Low** | Автоматическая генерация |
| ✅ Производительность | **Low** | Бенчмарки |

---

## Итог: минимальный набор тестов для PersonId

```csharp
public class PersonIdMinimumTests
{
    // 1. Создание
    [Fact] public void Create_Valid_Works() { }
    [Fact] public void Create_Invalid_Throws() { }
    
    // 2. Равенство
    [Fact] public void Equals_SameValue_True() { }
    [Fact] public void Equals_DifferentValue_False() { }
    [Fact] public void GetHashCode_SameValue_Equals() { }
    
    // 3. Неизменяемость
    [Fact] public void IsImmutable_ValueCannotChange() { }
    
    // 4. Преобразования
    [Fact] public void ImplicitConversion_Works() { }
    [Fact] public void ToString_ReturnsValue() { }
    
    // 5. Использование
    [Fact] public void CanBeUsedInDictionary() { }
    [Fact] public void CanBeUsedInHashSet() { }
}
```

---

## Заключение

**Тесты для PersonId - обязательно!** Это не просто "хорошая практика", а критически важный элемент:

- ✅ **Value Object - основа модели** - ошибка = вся модель сломана
- ✅ **Идентификация** - неправильный ID = потеря данных
- ✅ **Коллекции** - неправильный Equals/GetHashCode = баги в Dictionary/HashSet
- ✅ **Безопасность** - валидация входных данных
- ✅ **Сериализация** - работа с API и БД

**Минимум:** 10-15 тестов для каждого типа PersonId (int и/или GUID).  
**Рекомендуемый минимум:** включить тесты для всех критических сценариев из чеклиста.

[Примеры тестов для PersonId](.\Examples\PersonalIdTests_Example.md)