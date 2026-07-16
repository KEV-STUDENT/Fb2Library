using Fb2Library.Application;
using Fb2Library.Domain;
using Fb2Library.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Настройка Cookie - все куки Secure и SameSite=None для кросс-портовых запросов
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always;
    options.MinimumSameSitePolicy = SameSiteMode.None; // None для разных портов
});

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None; // Критично: None для работы между портами
    options.Cookie.HttpOnly = true;
    // Для SameSite=None требуется Secure
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7029; // Порт HTTPS для API
});

WebApplication app = builder.Build();

// Строгий HTTPS
app.UseHttpsRedirection();
app.UseCookiePolicy();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy" }));

app.Run();
