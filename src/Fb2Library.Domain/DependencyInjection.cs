using Microsoft.Extensions.DependencyInjection;

namespace Fb2Library.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            // Доменные сервисы
            //services.AddScoped<BookDomainService>();
            //services.AddScoped<GenreDomainService>();

            // Диспетчер доменных событий
            //services.AddScoped<DomainEventDispatcher>();

            return services;
        }
    }
}
