# 💡 Описание используемых технологий

Используется стэк **xUnit + FluentAssertions + NSubstitute** :
- **xUnit** — самый современный фреймворк для .NET. В отличие от NUnit/MSTest, использует конструктор для setup и disposable для cleanup (нет [SetUp]/[TearDown] атрибутов), что делает тесты чище и ближе к pure functions.
- **FluentAssertions** — читаемые ассерты в стиле человеческого языка. Вместо Assert.Equal(expected, actual) пишем actual.Should().Be(expected) — читается как предложение.
- **NSubstitute** — самый простой мок-фреймворк. Не требует различать stub/mock/fake, всё создается через Substitute.For<T>().

# 🎯 Установка

dotnet add package xunit  
dotnet add package xunit.runner.visualstudio  # чтобы тесты запускались в IDE  
dotnet add package FluentAssertions  
dotnet add package NSubstitute  
dotnet add package NSubstitute.Analyzers.CSharp # анализатор для предотвращения ошибок  
dotnet add package Microsoft.NET.Test.Sdk       # инфраструктура для запуска тестов  
dotnet add package coverlet.collector           # для измерения покрытия кода  

[Тесты для ValueObjects представляющие идентификаторы сущностей](.\Tests\IdentityTests.md)
