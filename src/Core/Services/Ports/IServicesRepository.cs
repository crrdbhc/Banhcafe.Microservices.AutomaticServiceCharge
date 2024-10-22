using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Models;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Ports
{   
    public interface IServicesRepository: IGenericRepository<ServicesBase, ViewServicesDto, CreateServicesDto, UpdateServicesDto, DeleteServicessDto>
    {
        Task<IEnumerable<ServicesBase>> ListAll (
            ViewAllServicesDto filtersDto,
            CancellationToken cancellationToken
        );

        Task<IEnumerable<ServicesBase>> List(
            ViewServicesDto filtersDto,
            CancellationToken cancellationToken
        );

        Task<ServicesBase> Create(
            CreateServicesDto dto,
            CancellationToken cancellationToken
        );

        Task<ServicesBase> Update(
            UpdateServicesDto dto,
            CancellationToken cancellationToken
        );

        Task<ServicesBase> Update(
            UpdateServiceSubscriptionsDto dto,
            CancellationToken cancellationToken
        );

        Task<ServicesBase> Update(
            UpdateSubscriptionsPaymentsDto dto,
            CancellationToken cancellationToken
        );
    }

}