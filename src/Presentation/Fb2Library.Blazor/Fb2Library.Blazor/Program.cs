using Fb2Library.Blazor.Components;
using Fb2Library.Infrastructure;
using Fb2Library.Application;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Конфигурация HTTPS
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7236;
});

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None; // Для кросс-портовых запросов
});

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// HttpClient для пререндеринга - используем HTTPS
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7236/") // HTTPS!
});

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

WebApplication app = builder.Build();

// Принудительный HTTPS
app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts(); // HTTP Strict Transport Security
}
else
{
    // Для разработки отключаем HSTS
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.MapStaticAssets();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Fb2Library.Blazor.Client._Imports).Assembly);

app.MapReverseProxy();

app.Run();
