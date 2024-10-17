using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Ports
{
    public interface ISubscriptionsRepository: IGenericRepository<SubscriptionsBase, ViewSubscriptionsDto, CreateSubscriptionsDto>
    {
        Task<SubscriptionsBase> Create(CreateSubscriptionsDto dto, CancellationToken cancellationToken);
        Task<IEnumerable<SubscriptionsBase>> List(
            ViewSubscriptionsDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}