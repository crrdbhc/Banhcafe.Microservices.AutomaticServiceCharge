using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Models;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Ports
{
    public interface IUserSubscriptionsRepository: IGenericRepository<UserSubscriptionsBase, ViewUserSubscriptionsDto, CreateUserSubscriptionsDto, UpdateUserSubscriptionsDto, DeleteUserSubscriptionsDto>
    {
        Task<UserSubscriptionsBase> Create(CreateUserSubscriptionsDto dto, CancellationToken cancellationToken);
        Task<IEnumerable<UserSubscriptionsBase>> List(
            ViewUserSubscriptionsDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}