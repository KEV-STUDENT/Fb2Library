using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fb2Library.Application.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
        private readonly PerformanceThresholdOptions _options;
        private readonly IMetricsService? _metrics; // опционально для сбора метрик

        public PerformanceBehavior(
            ILogger<PerformanceBehavior<TRequest, TResponse>> logger,
            IOptions<PerformanceThresholdOptions> options,
            IMetricsService? metrics = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options?.Value ?? new PerformanceThresholdOptions();
            _metrics = metrics;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var stopwatch = Stopwatch.StartNew();

            try
            {
                TResponse? response = await next().ConfigureAwait(false);
                stopwatch.Stop();

                await LogPerformance(requestName, stopwatch.ElapsedMilliseconds, null).ConfigureAwait(false);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                // Логируем даже при ошибках, так как медленные ошибки тоже важны
                await LogPerformance(requestName, stopwatch.ElapsedMilliseconds, ex).ConfigureAwait(false);

                throw;
            }
        }

        private async Task LogPerformance(string requestName, long elapsedMs, Exception? exception)
        {
            // Настраиваемые пороги для разных типов запросов
            var threshold = GetThreshold(requestName);

            if (elapsedMs <= threshold) return;

            LogLevel logLevel = elapsedMs switch
            {
                > 5000 => LogLevel.Error,    // Критически медленно
                > 2000 => LogLevel.Warning,   // Очень медленно
                > 1000 => LogLevel.Information, // Просто медленно
                _ => LogLevel.Debug
            };

            if (exception != null)
            {
                _logger.Log(
                    logLevel,
                    exception,
                    "Slow request {RequestName} failed after {ElapsedMs}ms (threshold: {ThresholdMs}ms)",
                    requestName, elapsedMs, threshold);
            }
            else
            {
                _logger.Log(
                    logLevel,
                    "Slow request {RequestName} took {ElapsedMs}ms (threshold: {ThresholdMs}ms)",
                    requestName, elapsedMs, threshold);
            }

            // Отправляем метрики в систему мониторинга
            _metrics?.RecordRequestDuration(requestName, elapsedMs);
        }

        private long GetThreshold(string requestName)
        {
            // Можно настроить разные пороги для разных запросов
            return _options.Thresholds?.TryGetValue(requestName, out var threshold) == true
                ? threshold
                : _options.DefaultThresholdMs;
        }
    }

    // Конфигурация
    public class PerformanceThresholdOptions
    {
        public long DefaultThresholdMs { get; set; } = 500;

        // Специфичные пороги для определенных запросов
        public Dictionary<string, long> Thresholds { get; set; } = new()
        {
            ["ExportLargeDataQuery"] = 5000,    // Экспорт данных - ожидаемо долго
            ["GenerateReportCommand"] = 3000,   // Генерация отчетов
            ["SimpleGetQuery"] = 200            // Простые запросы должны быть быстрыми
        };
    }

    // Интерфейс для метрик (пример)
    public interface IMetricsService
    {
        public void RecordRequestDuration(string requestName, long elapsedMs);
    }
}
