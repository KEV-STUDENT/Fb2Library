using Fb2Library.Application;
using Fb2Library.Blazor.Components;
using Fb2Library.Blazor.Extensions;
using Fb2Library.Blazor.Services;
using Fb2Library.Domain;
using Fb2Library.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Polly;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// 1. API контроллеры
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Blazor с WebAssembly
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// 3. HttpClient с Retry политикой (исправленная версия)
builder.Services.AddHttpClient("ServerApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7236/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
})
.AddTransientHttpErrorPolicy(policy =>
    policy.WaitAndRetryAsync(3, retryAttempt =>
        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
.AddRetryPolicy();

// Для использования в компонентах:
builder.Services.AddScoped<HttpClient>(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerApi"));

// 4. Слои приложения
builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// 5. Сервис для передачи состояния
builder.Services.AddScoped<AppState>();

// 6. Antiforgery с учетом окружения
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Strict;

    if (builder.Environment.IsDevelopment())
    {
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.SuppressXFrameOptionsHeader = false;
    }
    else
    {
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    }
});

// 7. CORS setup
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorWasm", policy =>
    {
        policy.WithOrigins(builder.Configuration["AllowedOrigins"] ?? "https://localhost:7236")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// 8. Response Compression (только для production)
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    });
}

// 9. Добавим Health Checks
builder.Services.AddHealthChecks();

WebApplication app = builder.Build();

// ===== НАСТРОЙКА PIPELINE (Порядок важен!) =====

// 1. Обработка ошибок (самая первая)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// 2. COOP/COEP заголовки для WebAssembly SharedArrayBuffer
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Cross-Origin-Opener-Policy", "same-origin");
    context.Response.Headers.Append("Cross-Origin-Embedder-Policy", "require-corp");

    // Дополнительные заголовки безопасности
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY");

    await next().ConfigureAwait(false);
});

// 3. HTTPS (только production)
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseResponseCompression();
}

// 4. CORS
app.UseCors("BlazorWasm");

// 5. Статические файлы
app.UseStaticFiles();
app.MapStaticAssets();

// 6. Antiforgery
app.UseAntiforgery();

// 7. Swagger (только development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 8. Health Checks
app.MapHealthChecks("/health");

// 9. API маршруты
app.MapControllers();

// 10. Blazor компоненты (должны быть последними)
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Fb2Library.Blazor.Client._Imports).Assembly);

app.Run();
