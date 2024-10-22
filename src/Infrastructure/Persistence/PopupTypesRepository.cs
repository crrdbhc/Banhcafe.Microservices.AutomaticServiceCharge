using System.Text.Json;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Ports;
using BANHCAFE.Cross.DBConnection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Persistence;

public class PopupTypesRepository(
    ILogger<PopupTypesRepository> logger,
    ISqlDbConnectionApiExtensions<object, IEnumerable<PopupTypesBase>> api,
    IOptionsMonitor<DatabaseSettings> dbSettingsMonitor    
) : IPopupTypesRepository
{
    private readonly DatabaseSettings _dbSettings = dbSettingsMonitor.Get(
        DatabaseSettingsInstances.SQL
    );

    internal const string GetAllCommand = "SP_SELALL_POPUPTYPES";
    internal const string CreateCommand = "SP_INS_POPUPTYPES";
    internal const string UpdateCommand = "SP_UPD_POPUPTYPE";

    public async Task<IEnumerable<PopupTypesBase>> ListAll (
        ViewAllPopupTypesDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameCat,
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

    public async Task<PopupTypesBase> View (
        ViewAllPopupTypesDto dto,
        CancellationToken cancellationToken
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameCat,
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
        return response.FirstOrDefault();
    }

    public async Task<PopupTypesBase> Create (
        CreatePopupTypesDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameCat,
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
    public async Task<PopupTypesBase> Update(
        UpdatePopupTypesDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SqlDbApiRequest<object>
        {
            Scheme = _dbSettings.SchemeNameCat,
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

    public Task<IEnumerable<PopupTypesBase>> List(ViewAllPopupTypesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }


    public Task<PopupTypesBase> Delete(DeletePopupTypesDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}