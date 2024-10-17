using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Ports
{   
    public interface IServicesRepository: IGenericRepository<ServicesBase, ViewServicesDto, CreateServicesDto>
    {
        Task<IEnumerable<ServicesBase>> List(
            ViewServicesDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}