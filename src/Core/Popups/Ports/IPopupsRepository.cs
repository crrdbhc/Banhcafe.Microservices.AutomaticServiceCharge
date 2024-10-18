using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Ports
{
    public interface IPopupsRepository: IGenericRepository<PopupsBase, ViewPopupsDto, CreatePopupsDto>
    {
        Task<IEnumerable<PopupsBase>> ListAll (
            ViewAllPopupsDto filtersDto,
            CancellationToken cancellationToken
        );

        Task<IEnumerable<PopupsBase>> List(
            ViewPopupsDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}