// Fb2Library.Application/DependencyInjection.cs
//using Fb2Library.Application.Handlers;
//using Fb2Library.Application.Services;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Fb2Library.Application.Behaviors;
using Fb2Library.Application.Interfaces;
using Fb2Library.Application.Services;


namespace Fb2Library.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // FluentValidation.DependencyInjectionExtensions даёт:
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // 1. Error Handling - самый внешний, ловит все ошибки
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));

            // 2. Logging - логирует все запросы/ответы
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            // 3. Performance - измеряет время выполнения
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

            // 4. Validation - проверяет запрос перед выполнением
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // 5. Caching - возвращает кешированный результат (пропускает handler)
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

            // 6. Authorization - проверяет права доступа
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            // 7. Transaction - управляет транзакцией (самый внутренний)
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));


            // Application Services
            services.AddScoped<IDocumentService, DocumentService>();
            //services.AddScoped<BookImportService>();
            //services.AddScoped<IBookApplicationService, BookApplicationService>();
            //services.AddScoped<IGenreApplicationService, GenreApplicationService>();

            return services;
        }
    }
}
