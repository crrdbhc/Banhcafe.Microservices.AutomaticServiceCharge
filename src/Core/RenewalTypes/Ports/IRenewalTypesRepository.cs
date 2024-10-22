using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.RenewalTypes.Models;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.RenewalTypes.Ports
{   
    public interface IRenewalTypesRepository: IGenericRepository<RenewalTypesBase, ViewRenewalTypesDto, CreateRenewalTypesDto, UpdateRenewalTypesDto, DeleteRenewalTypesDto>
    {
        Task<IEnumerable<RenewalTypesBase>> List(
            ViewRenewalTypesDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}