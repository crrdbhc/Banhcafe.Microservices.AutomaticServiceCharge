using System.Text.Json;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Ports;
using BANHCAFE.Cross.DBConnection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Persistence;
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