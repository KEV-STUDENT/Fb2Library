using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Регистрируем HttpClient для запросов из WebAssembly к вашему API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7029/") // URL вашего Fb2Library.Api без подпапок!
});

await builder.Build().RunAsync().ConfigureAwait(false);
