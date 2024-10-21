using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.RenewalTypes.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.RenewalTypes.Ports
{   
    public interface IRenewalTypesRepository: IGenericRepository<RenewalTypesBase, ViewRenewalTypesDto, CreateRenewalTypesDto, UpdateRenewalTypesDto, DeleteRenewalTypesDto>
    {
        Task<IEnumerable<RenewalTypesBase>> List(
            ViewRenewalTypesDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}