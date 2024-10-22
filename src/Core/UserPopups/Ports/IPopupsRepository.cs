using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Models;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Ports
{
    public interface IUserPopupsRepository: IGenericRepository<UserPopupsBase, ViewUserPopupsDto, HideUserPopupDto, UpdateUserPopupsDto, DeleteUserPopupsDto>
    {
        Task<UserPopupsBase> Create(HideUserPopupDto dto, CancellationToken cancellationToken);
        Task<IEnumerable<UserPopupsBase>> List(
            ViewUserPopupsDto filtersDto,
            CancellationToken cancellationToken
        );
    }

}
