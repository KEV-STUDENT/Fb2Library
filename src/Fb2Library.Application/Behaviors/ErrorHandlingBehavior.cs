using MediatR;
using Microsoft.Extensions.Logging;
using Fb2Library.Application.Exceptions;

namespace Fb2Library.Application.Behaviors
{
    public class ErrorHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ErrorHandlingBehavior<TRequest, TResponse>> _logger;

        public ErrorHandlingBehavior(ILogger<ErrorHandlingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next().ConfigureAwait(false);
            }
            catch (ValidationException)
            {
                // Ошибки валидации - возвращаем как есть (400 Bad Request)
                throw;
            }
            catch (NotFoundException ex)
            {
                // Not Found - логируем как информацию (404 Not Found)
                _logger.LogInformation(ex, "Resource not found: {RequestName}", typeof(TRequest).Name);
                throw;
            }
            catch (BusinessException ex)
            {
                // Бизнес-ошибки - логируем как предупреждение (422 Unprocessable Entity)
                _logger.LogWarning(ex, "Business-error: {RequestName}, Code: {ErrorCode}",
                    typeof(TRequest).Name, ex.ErrorCode);
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                // Ошибки доступа (403 Forbidden)
                _logger.LogWarning(ex, "Access denided: {RequestName}", typeof(TRequest).Name);
                throw new BusinessException("Access denided", "FORBIDDEN");
            }
            catch (OperationCanceledException ex) when (cancellationToken.IsCancellationRequested)
            {
                // Отмена операции
                _logger.LogInformation("Operation cancelled: {RequestName} {ErrorMessage}", typeof(TRequest).Name, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                // Неожиданные ошибки (500 Internal Server Error)
                _logger.LogError(ex, "Critical error: {RequestName}, Request: {@Request}",
                    typeof(TRequest).Name, request);

                // В production не раскрываем детали ошибки
                throw new BusinessException(
                    "Internal error. Try latter.", "INTERNAL_ERROR");
            }
        }
    }
}
