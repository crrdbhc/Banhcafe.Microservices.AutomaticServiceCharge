using System.Text.Json;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Commands.Update;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Ports;
using BANHCAFE.Cross.DBConnection;
using Microsoft.Extensions.Http.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Persistence;

public class ServicesRepository (
    ILogger<ServicesRepository> logger,
    ISqlDbConnectionApiExtensions<object, IEnumerable<ServicesBase>> api,
    IOptionsMonitor<DatabaseSettings> dbSettingsMonitor
) : IServicesRepository
{
    private readonly DatabaseSettings _dbSettings = dbSettingsMonitor.Get(
        DatabaseSettingsInstances.SQL
    );

    internal const string QueryCommand = "SP_SEL_ALLSERVICEDATABYID";
    internal const string GetAllCommand = "SP_SELLALL_SERVICETYPES";
    internal const string CreateCommand = "SP_INS_SERVICETYPE";
    internal const string UpdateCommand = "SP_UPD_SERVICETYPE";
    internal const string UpdateServiceSubscriptionCommand = "SP_UPD_SUBSCRIPTIONSERVICES";
    internal const string UpdateSubscriptionPaymentsCommand = "SP_UPD_SUBSCRIPTIONPAYMENTS";
    // internal const string CreateCommand = "SP_PRUEBAS";

    public async Task<IEnumerable<ServicesBase>> ListAll(
        ViewAllServicesDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameServ,
            Database = _dbSettings.DatabaseName,
            StoredProcedure = GetAllCommand,
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

    public async Task<ServicesBase> Create (
        CreateServicesDto dto,
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

    public async Task<ServicesBase> Update (
        UpdateServicesDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameServ,
            Database = _dbSettings.DatabaseName,
            StoredProcedure = UpdateCommand,
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

    public async Task<ServicesBase> Update(
        UpdateServiceSubscriptionsDto dto, 
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameServ,
            Database = _dbSettings.DatabaseName,
            StoredProcedure = UpdateServiceSubscriptionCommand,
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

    public async Task<ServicesBase> Update(
        UpdateSubscriptionsPaymentsDto dto,
        CancellationToken cancellationToken
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameServ,
            Database = _dbSettings.DatabaseName,
            StoredProcedure = UpdateSubscriptionPaymentsCommand,
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
        
        var response = await api.Process(logger, request, cancellationToken );
        return response.FirstOrDefault();
    }

    public Task<ServicesBase> Delete(DeleteServicessDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}