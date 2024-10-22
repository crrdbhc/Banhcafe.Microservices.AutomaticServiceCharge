using System.Text.Json;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.RenewalTypes.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.RenewalTypes.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Ports;
using BANHCAFE.Cross.DBConnection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Persistence;

public class RenewalTypesRepository (
    ILogger<RenewalTypesRepository> logger,
    ISqlDbConnectionApiExtensions<object, IEnumerable<RenewalTypesBase>> api,
    IOptionsMonitor<DatabaseSettings> dbSettingsMonitor
) : IRenewalTypesRepository
{
    private readonly DatabaseSettings _dbSettings = dbSettingsMonitor.Get(
        DatabaseSettingsInstances.SQL
    );

    internal const string QueryCommand = "SP_SELALL_RENEWALTYPES";

    public async Task<IEnumerable<RenewalTypesBase>> List(
        ViewRenewalTypesDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameCat,
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

    public async Task<RenewalTypesBase> View (
        ViewRenewalTypesDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameCat,
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

    public Task<RenewalTypesBase> Create(CreateRenewalTypesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<RenewalTypesBase> Update(UpdateRenewalTypesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<RenewalTypesBase> Delete(DeleteRenewalTypesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}