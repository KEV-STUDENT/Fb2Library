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
            // Парсеры
            services.AddScoped<IDocumentParser, Fb2Parser>();
            services.AddScoped<IDocumentParserFactory, DocumentParserFactory>();

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
