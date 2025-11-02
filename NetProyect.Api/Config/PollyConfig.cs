using Polly;
using Polly.Extensions.Http;

namespace NetProyect.Api.Config;
public static class PollyConfig
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
        HttpPolicyExtensions.HandleTransientHttpError()
            .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)));

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreaker() =>
        HttpPolicyExtensions.HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
}