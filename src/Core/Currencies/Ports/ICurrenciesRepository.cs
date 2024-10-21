using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Currencies.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Currencies.Ports
{   
    public interface ICurrenciesRepository: IGenericRepository<CurrenciesBase, ViewCurrenciesDto, CreateCurrenciesDto, UpdateCurrenciesDto, DeleteCurrenciesDto>
    {
        Task<IEnumerable<CurrenciesBase>> List(
            ViewCurrenciesDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}