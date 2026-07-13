using Fb2Library.Domain.Shared.Interfaces;
using Fb2Library.Infrastructure.Parsing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fb2Library.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // База данных
            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("DefaultConnection")));

            // Репозитории (Domain интерфейсы → Infrastructure реализации)
            //services.AddScoped<IBookRepository, BookRepository>();
            //services.AddScoped<IGenreRepository, GenreRepository>();
            //services.AddScoped<IAuthorRepository, AuthorRepository>();

            // Парсеры
            services.AddScoped<IDocumentParser, Fb2Parser>();
            services.AddScoped<IDocumentParserFactory, DocumentParserFactory>();

            // Unit of Work
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure();
            services.Configure<ParserOptions>(configuration.GetSection("Parsing"));

            return services;
        }
    }
}
