using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Ports
{
    public interface IUserPopupsRepository: IGenericRepository<UserPopupsBase, ViewUserPopupsDto, HideUserPopupDto>
    {
        Task<UserPopupsBase> Create(HideUserPopupDto dto, CancellationToken cancellationToken);
        Task<IEnumerable<UserPopupsBase>> List(
            ViewUserPopupsDto filtersDto,
            CancellationToken cancellationToken
        );
    }

}
