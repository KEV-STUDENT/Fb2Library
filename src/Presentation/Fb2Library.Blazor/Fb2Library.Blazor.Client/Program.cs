using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fb2Library.Infrastructure;
using Fb2Library.Application;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// HttpClient с базовым адресом Blazor.Server
// Все запросы к /api/* будут проксироваться через YARP
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

await builder.Build().RunAsync().ConfigureAwait(false);
