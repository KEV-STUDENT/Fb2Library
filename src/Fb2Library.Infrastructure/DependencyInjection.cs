using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fb2Library.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
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
            //services.AddScoped<IFb2ParserService, Fb2ParserService>();

            // Unit of Work
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
