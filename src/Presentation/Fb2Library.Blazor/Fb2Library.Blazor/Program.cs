using Fb2Library.Blazor.Components;
using Fb2Library.Infrastructure; // Подключите пространства имен ваших слоев
using Fb2Library.Application;
//using Fb2Library.Blazor.Client;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// 1. Оставляем ТОЛЬКО поддержку Razor Components и WebAssembly.
// СТРОКУ .AddInteractiveServerComponents() НУЖНО УДАЛИТЬ!
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

//Prerendering
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

// Ваш HttpClient для API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7029/")
});

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.Use((context, next) =>
{
    context.Response.Headers.Append("Cross-Origin-Opener-Policy", "same-origin");
    context.Response.Headers.Append("Cross-Origin-Embedder-Policy", "require-corp");
    return next();
});

app.MapStaticAssets();
app.UseStaticFiles();

// 2. В маппинге тоже убираем .AddInteractiveServerRenderMode()
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode() // Оставляем только WASM
    .AddAdditionalAssemblies(typeof(Fb2Library.Blazor.Client._Imports).Assembly);

app.Run();
