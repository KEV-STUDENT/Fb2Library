# 📚 Fb2Library

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-purple)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![WPF](https://img.shields.io/badge/WPF-.NET_Desktop-green)](https://dotnet.microsoft.com/en-us/apps/desktop)

**Fb2Library** — учебный проект по созданию библиотеки для хранения, управления и чтения книг в формате FB2.

---

## 🎯 Цели проекта

- Изучить и применить на практике **Clean Architecture**.
- Освоить подход **TDD** (Test-Driven Development)
- Разработать два варианта пользовательского интерфейса: **Blazor Server** (веб) и **WPF** (десктоп).
- Научиться работать с **Entity Framework Core** (SQLite / MS SQL Server).
- Реализовать полноценный парсинг, поиск и чтение FB2-файлов.

---

## 🛠 Технологический стек

| Компонент | Технология |
| :--- | :--- |
| **Язык** | C# 12 |
| **Платформа** | .NET 10 (LTS) |
| **Архитектура** | Clean Architecture |
| **ORM** | Entity Framework Core |
| **База данных** | SQLite (разработка), MS SQL Server (опционально) |
| **Веб-интерфейс** | Blazor Server |
| **Десктоп-интерфейс** | WPF + MVVM |
| **Парсинг FB2** | System.Xml.Linq |

---

## 📂 Структура проекта

```text
Fb2Library/
├── Fb2Library.sln
├── src/
│   ├── Fb2Library.Domain/              # Ядро: сущности, интерфейсы
│   ├── Fb2Library.Application/         # Бизнес-логика: Use Cases, DTO
│   ├── Fb2Library.Infrastructure/      # ИНФРАСТРУКТУРА ПАРСИНГА: Только Fb2.Document и System.IO.Compression
│   ├── Fb2Library.Persistence/         # ИНФРАСТРУКТУРА СЕРВЕРА: EF Core, БД, миграции
│   └── Presentation/
│       ├── Fb2Library.Blazor/          # Blazor Host (подключает Blazor.Client + BackEnd)
│       ├── Fb2Library.Blazor.Client/   # Веб-интерфейс (Blazor - WASM)
│       └── Fb2Library.Wpf/             # Десктоп-интерфейс (WPF) — позже
|
├── tests/                              # Тесты
│   ├── Fb2Library.Domain.Tests/
│   ├── Fb2Library.Application.Tests/
│   └── Fb2Library.Integration.Tests/
└── docs/                               # Детальная документация   
