// Fb2Library.Application/DependencyInjection.cs
//using Fb2Library.Application.Handlers;
//using Fb2Library.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fb2Library.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // MediatR (если используете)
            //services.AddMediatR(cfg =>
                //cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // Application Services
            //services.AddScoped<BookImportService>();
            //services.AddScoped<IBookApplicationService, BookApplicationService>();
            //services.AddScoped<IGenreApplicationService, GenreApplicationService>();

            return services;
        }
    }
}
