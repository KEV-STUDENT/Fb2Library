using Fb2Library.Application;
using Fb2Library.Domain;
using Fb2Library.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAntiforgery();

// 1. ИСПРАВЛЕНИЕ CORS: Разрешаем передачу Cookie (Credentials)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7236") // URL вашего Blazor-клиента
              .AllowAnyMethod()
              // Заменяем AllowAnyHeader на явное перечисление базовых заголовков для безопасности
              .WithHeaders("Content-Type", "Authorization", "Accept")
              .AllowCredentials(); // КРИТИЧНО ДЛЯ COOKIE: разрешает отправку кук между портами
    });
});

// 2. ИСПРАВЛЕНИЕ COOKIE: Принудительно делаем все куки безопасными
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always; // Только по HTTPS
    options.MinimumSameSitePolicy = SameSiteMode.Lax; // Позволяет передавать куки на localhost
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Слои приложения
builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Перехватываем настройки Antiforgery в самый последний момент сборки приложения
builder.Services.PostConfigure<Microsoft.AspNetCore.Antiforgery.AntiforgeryOptions>(options =>
{
    // Заставляем куку ВСЕГДА быть Secure
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    // КРИТИЧНО: Принудительно меняем Strict на None для работы между портами
    options.Cookie.SameSite = SameSiteMode.None;
});


WebApplication app = builder.Build();

// 1. ПЕРВЫМ ДЕЛОМ: Разрешаем CORS, чтобы браузер вообще соглашался принимать заголовки
app.UseCors("AllowBlazor");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 3. ИСПРАВЛЕНИЕ: Добавляем применение политики Cookie сразу после CORS
app.UseCookiePolicy();

app.UseHttpsRedirection();
app.MapControllers();

// Health check
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy" }));

app.Run();
