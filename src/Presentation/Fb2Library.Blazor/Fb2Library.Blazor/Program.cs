using Fb2Library.Blazor.Components;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// 1. Оставляем ТОЛЬКО поддержку Razor Components и WebAssembly.
// СТРОКУ .AddInteractiveServerComponents() НУЖНО УДАЛИТЬ!
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

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
app.MapStaticAssets();

// 2. В маппинге тоже убираем .AddInteractiveServerRenderMode()
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode() // Оставляем только WASM
    .AddAdditionalAssemblies(typeof(Fb2Library.Blazor.Client._Imports).Assembly);

app.Run();
