using Fb2Library.Application;
using Fb2Library.Domain;
using Fb2Library.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// CORS - разрешаем запросы от Blazor
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7234") // URL Blazor
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Слои приложения
builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazor");
app.UseHttpsRedirection();
app.MapControllers();

// Health check
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy" }));

app.Run();
