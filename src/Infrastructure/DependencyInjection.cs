using System.Net;
using System.Net.Http.Headers;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Currencies.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Currencies.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.RenewalTypes.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.RenewalTypes.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Timeout;
using Refit;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure;
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

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<UserPopupsBase>>>(
            settingsAction: (sp) => new() { },
            httpClientName: "DBClient"
        );

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<UserSubscriptionsBase>>> (
            settingsAction: (sp) => new() {},
            httpClientName: "DBClient"
        );

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<UserServicesBase>>>(
            settingsAction: (sp) => new() {},
            httpClientName: "DBClient"
        );

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<ServicesBase>>>(
            settingsAction: (sp) => new() {},
            httpClientName: "DBClient"
        );

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<PopupsBase>>>(
            settingsAction: (sp) => new() { },
            httpClientName: "DBClient"
        );

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<PopupTypesBase>>>(
            settingsAction: (sp) => new() {},
            httpClientName: "DBClient"
        );

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<CurrenciesBase>>>(
            settingsAction: (sp) => new() {},
            httpClientName: "DBClient"
        );

        services.AddRefitClient<ISqlDbConnectionApiExtensions<object, IEnumerable<RenewalTypesBase>>>(
            settingsAction: (sp) => new() {},
            httpClientName: "DBClient"
        );

        services.AddScoped<IUserPopupsRepository, UserPopupsRepository>();
        services.AddScoped<IUserSubscriptionsRepository, UserSubscriptionsRepository>();
        services.AddScoped<IUserServicesRepository, UserServicesRepository>();
        services.AddScoped<IServicesRepository, ServicesRepository>();
        services.AddScoped<IPopupsRepository, PopupsRepository>();
        services.AddScoped<IPopupTypesRepository, PopupTypesRepository>();
        services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
        services.AddScoped<IRenewalTypesRepository, RenewalTypesRepository>();

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
