using System.Text.Json;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Common.Ports;
using BANHCAFE.Cross.DBConnection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Persistence;

public class ServicesRepository (
    ILogger<ServicesRepository> logger,
    ISqlDbConnectionApiExtensions<object, IEnumerable<ServicesBase>> api,
    IOptionsMonitor<DatabaseSettings> dbSettingsMonitor
) : IServicesRepository
{
    private readonly DatabaseSettings _dbSettings = dbSettingsMonitor.Get(
        DatabaseSettingsInstances.SQL
    );

    internal const string QueryCommand = "SP_SELALL_AVAILABLESUBSCRIPTIONS";

    public async Task<IEnumerable<ServicesBase>> List(
        ViewServicesDto dto,
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

    public async Task<ServicesBase> View (
        ViewServicesDto dto,
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

    public Task<ServicesBase> Create(CreateServicesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}