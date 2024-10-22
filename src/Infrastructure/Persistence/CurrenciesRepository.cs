using System.Text.Json;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Currencies.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Currencies.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Common.Ports;
using BANHCAFE.Cross.DBConnection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Infrastructure.Persistence;

public class CurrenciesRepository (
    ILogger<CurrenciesRepository> logger,
    ISqlDbConnectionApiExtensions<object, IEnumerable<CurrenciesBase>> api,
    IOptionsMonitor<DatabaseSettings> dbSettingsMonitor
) : ICurrenciesRepository
{
    private readonly DatabaseSettings _dbSettings = dbSettingsMonitor.Get(
        DatabaseSettingsInstances.SQL
    );

    internal const string QueryCommand = "SP_SELALL_CURRENCIES";

    public async Task<IEnumerable<CurrenciesBase>> List(
        ViewCurrenciesDto dto,
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

    public async Task<CurrenciesBase> View (
        ViewCurrenciesDto dto,
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

    public Task<CurrenciesBase> Create(CreateCurrenciesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<CurrenciesBase> Update(UpdateCurrenciesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<CurrenciesBase> Delete(DeleteCurrenciesDto dto = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}