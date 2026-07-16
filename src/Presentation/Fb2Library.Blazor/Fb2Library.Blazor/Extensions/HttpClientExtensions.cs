using Polly;

namespace Fb2Library.Blazor.Extensions
{
    public static class HttpClientExtensions
    {
        public static IHttpClientBuilder AddRetryPolicy(this IHttpClientBuilder builder)
        {
            return builder.AddTransientHttpErrorPolicy(policy =>
                policy.WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
                        // Здесь можно добавить логирование
                        Console.WriteLine(
                            $"Retry {retryAttempt} after {timespan.TotalSeconds}s due to {outcome.Exception?.Message}");
                    }));
        }
    }
}
