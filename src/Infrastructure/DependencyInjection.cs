using System.Net;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Timeout;
using Refit;

namespace Banhcafe.Microservices.ServiceChargingSystem.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        _ = services
            .AddHttpClient(
                "DBClient",
                static (sp, client) =>
                {
                    var dbSettings = sp.GetRequiredService<IOptionsMonitor<DatabaseSettings>>();
                    client.BaseAddress = new Uri(
                        dbSettings.Get(DatabaseSettingsInstances.SQL).DatabaseApiUrl
                    );
                    client.Timeout = TimeSpan.FromMinutes(3);
                }
            )
            .ConfigurePrimaryHttpMessageHandler(PrimaryHttpMessageHandler)
            .AddResilienceHandler("db-client-pipeline", BasicHttpClientResiliencePipeline);

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<PopupsBase>>>(
            settingsAction: (sp) => new() { },
            httpClientName: "DBClient"
        );

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<SubscriptionsBase>>> (
            settingsAction: (sp) => new() {},
            httpClientName: "DBClient"
        );

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<ServicesBase>>>(
            settingsAction: (sp) => new() {},
            httpClientName: "DBClient"
        );

        services.AddScoped<IPopupsRepository, PopupsRepository>();
        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<IServicesRepository, ServicesRepository>();

        return services;
    }

    private static HttpMessageHandler PrimaryHttpMessageHandler()
    {
        return new HttpClientHandler() { MaxConnectionsPerServer = 256, UseProxy = false };
    }

    private static void BasicHttpClientResiliencePipeline(
        ResiliencePipelineBuilder<HttpResponseMessage> builder,
        ResilienceHandlerContext context
    )
    {
        // Refer to https://www.pollydocs.org/strategies/retry.html#defaults for retry defaults
        _ = builder
            .AddRetry(
                new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = 4,
                    UseJitter = true,
                    MaxDelay = TimeSpan.FromSeconds(5),
                    ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                        .Handle<TimeoutRejectedException>()
                        .Handle<ApiException>()
                        .Handle<HttpRequestException>()
                        .HandleResult(static response =>
                            response.StatusCode
                                is HttpStatusCode.BadRequest
                                    or HttpStatusCode.RequestTimeout
                                    or HttpStatusCode.TooManyRequests
                                    or HttpStatusCode.InternalServerError
                                    or HttpStatusCode.BadGateway
                                    or HttpStatusCode.ServiceUnavailable
                                    or HttpStatusCode.GatewayTimeout
                        ),
                    BackoffType = DelayBackoffType.Exponential
                }
            )
            .AddTimeout(new HttpTimeoutStrategyOptions() { Timeout = TimeSpan.FromMinutes(2), });
    }
}
