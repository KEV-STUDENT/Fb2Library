using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fb2Library.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var requestId = Guid.NewGuid().ToString("N");

            // Логируем детали запроса (если нужно)
            using IDisposable? scope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["RequestId"] = requestId,
                ["RequestName"] = requestName
            });

            _logger.LogInformation(
                "Processing request {RequestName} [RequestId: {RequestId}]",
                requestName,
                requestId);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                TResponse? response = await next(cancellationToken).ConfigureAwait(false);

                stopwatch.Stop();

                _logger.LogInformation(
                    "Completed request {RequestName} [RequestId: {RequestId}] in {ElapsedMilliseconds}ms",
                    requestName,
                    requestId,
                    stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(
                    ex,
                    "Request {RequestName} [RequestId: {RequestId}] failed after {ElapsedMilliseconds}ms",
                    requestName,
                    requestId,
                    stopwatch.ElapsedMilliseconds);

                throw;
            }
        }
    }
}
