using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fb2Library.Infrastructure;
using Fb2Library.Application;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Регистрируем HttpClient для запросов из WebAssembly к вашему API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7029/") // URL вашего Fb2Library.Api без подпапок!
});

// Регистрируем инфраструктуру
builder.Services.AddInfrastructure();  // ← Один метод вместо нескольких строк
// Регистрируем Application Layor
builder.Services.AddApplication();


await builder.Build().RunAsync().ConfigureAwait(false);
