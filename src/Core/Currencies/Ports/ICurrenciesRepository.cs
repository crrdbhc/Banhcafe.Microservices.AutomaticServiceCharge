using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Currencies.Models;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Currencies.Ports
{   
    public interface ICurrenciesRepository: IGenericRepository<CurrenciesBase, ViewCurrenciesDto, CreateCurrenciesDto, UpdateCurrenciesDto, DeleteCurrenciesDto>
    {
        Task<IEnumerable<CurrenciesBase>> List(
            ViewCurrenciesDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}