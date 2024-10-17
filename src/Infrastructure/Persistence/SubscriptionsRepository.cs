using System.Text.Json;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Common.Ports;
using BANHCAFE.Cross.DBConnection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Persistence;

public class SubscriptionsRepository (
    ILogger<SubscriptionsRepository> logger,
    ISqlDbConnectionApiExtensions<object, IEnumerable<SubscriptionsBase>> api,
    IOptionsMonitor<DatabaseSettings> dbSettingsMonitor
) : ISubscriptionsRepository
{
    private readonly DatabaseSettings _dbSettings = dbSettingsMonitor.Get(
        DatabaseSettingsInstances.SQL
    );

    internal const string QueryCommand = "SP_SEL_ALLSUBSCRIPTIONSBYUSERID";
    internal const string CreateCommand = "SP_INS_USERSUBSCRIPTION";

    public async Task<IEnumerable<SubscriptionsBase>> List(
        ViewSubscriptionsDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameServ,
            Database = _dbSettings.DatabaseName,
            StoredProcedure = QueryCommand,
            Parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(
                JsonSerializer.Serialize(
                    dto,
                    options: new()
                    {
                        DefaultIgnoreCondition = System
                            .Text
                            .Json
                            .Serialization
                            .JsonIgnoreCondition
                            .WhenWritingNull
                    }
                )
            )!,
        };

        var response = await api.Process(logger, request, cancellationToken);
        return response;
    }

    public async Task<SubscriptionsBase> View (
        ViewSubscriptionsDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameServ,
            Database = _dbSettings.DatabaseName,
            StoredProcedure = QueryCommand,
            Parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(
                JsonSerializer.Serialize(
                    dto,
                    options: new()
                    {
                        DefaultIgnoreCondition = System
                            .Text
                            .Json
                            .Serialization
                            .JsonIgnoreCondition
                            .WhenWritingNull
                    }
                )
            )!,
        };

        var response = await api.Process(logger, request, cancellationToken);
        return response.FirstOrDefault();
    }

    public async Task<SubscriptionsBase> Create (
        CreateSubscriptionsDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameServ,
            Database = _dbSettings.DatabaseName,
            StoredProcedure = CreateCommand,
            Parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(
                JsonSerializer.Serialize(
                    dto,
                    options: new()
                    {
                        DefaultIgnoreCondition = System
                            .Text
                            .Json
                            .Serialization
                            .JsonIgnoreCondition
                            .WhenWritingNull
                    }
                )
            )!,
        };

        var response = await api.Process(logger, request, cancellationToken);
        return response.FirstOrDefault();
    }
}