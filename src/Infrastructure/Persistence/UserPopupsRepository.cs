using System.Text.Json;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Common.Ports;
using BANHCAFE.Cross.DBConnection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Banhcafe.Microservices.ServiceChargingSystem.Infrastructure.Persistence;
public class UserPopupsRepository(
    ILogger<UserPopupsRepository> logger,
    ISqlDbConnectionApiExtensions<object, IEnumerable<UserPopupsBase>> api,
    IOptionsMonitor<DatabaseSettings> dbSettingsMonitor
) : IUserPopupsRepository
{
    private readonly DatabaseSettings _dbSettings = dbSettingsMonitor.Get(
        DatabaseSettingsInstances.SQL
    );

    internal const string QueryCommand = "SP_SEL_SHOWPOPUPSBYUSER";
    internal const string CreateCommand = "SP_INS_TOGGLEDONTSHOWPOPUP";

    public async Task<IEnumerable<UserPopupsBase>> List(
        ViewUserPopupsDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeName,
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

    public async Task<UserPopupsBase> View(
        ViewUserPopupsDto dto,
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

    public async Task<UserPopupsBase> Create (
        HideUserPopupDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeName,
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

    public Task<UserPopupsBase> Update(UpdateUserPopupsDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserPopupsBase> Delete(DeleteUserPopupsDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}