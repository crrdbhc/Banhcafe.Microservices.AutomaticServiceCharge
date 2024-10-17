using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserSubscriptions.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserSubscriptions.Ports
{
    public interface IUserSubscriptionsRepository: IGenericRepository<UserSubscriptionsBase, ViewUserSubscriptionsDto, CreateUserSubscriptionsDto>
    {
        Task<UserSubscriptionsBase> Create(CreateUserSubscriptionsDto dto, CancellationToken cancellationToken);
        Task<IEnumerable<UserSubscriptionsBase>> List(
            ViewUserSubscriptionsDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}