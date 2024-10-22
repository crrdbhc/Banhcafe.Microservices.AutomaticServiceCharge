using System.Text.Json;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Ports;
using BANHCAFE.Cross.DBConnection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Persistence;

public class UserServicesRepository (
    ILogger<UserServicesRepository> logger,
    ISqlDbConnectionApiExtensions<object, IEnumerable<UserServicesBase>> api,
    IOptionsMonitor<DatabaseSettings> dbSettingsMonitor
) : IUserServicesRepository
{
    private readonly DatabaseSettings _dbSettings = dbSettingsMonitor.Get(
        DatabaseSettingsInstances.SQL
    );

    internal const string QueryCommand = "SP_SELALL_AVAILABLESUBSCRIPTIONS";

    public async Task<IEnumerable<UserServicesBase>> List(
        ViewUserServicesDto dto,
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

    public async Task<UserServicesBase> View (
        ViewUserServicesDto dto,
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

    public Task<UserServicesBase> Create(CreateUserServicesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserServicesBase> Update(UpdateUserServicesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserServicesBase> Delete(DeleteUserServicesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}