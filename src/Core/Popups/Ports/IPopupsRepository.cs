using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Ports
{
    public interface IPopupsRepository: IGenericRepository<PopupsBase, ViewPopupsDto, CreatePopupsDto, UpdatePopupsDto, DeletePopupsDto>
    {
        Task<IEnumerable<PopupsBase>> ListAll (
            ViewAllPopupsDto filtersDto,
            CancellationToken cancellationToken
        );

        Task<IEnumerable<PopupsBase>> List(
            ViewPopupsDto filtersDto,
            CancellationToken cancellationToken
        );

        Task<PopupsBase> Create(
            CreatePopupsDto dto,
            CancellationToken cancellationToken
        );

        Task<PopupsBase> Update(
            UpdatePopupsDto dto,
            CancellationToken cancellationToken
        );

        Task<PopupsBase> Update(
            UpdatePopupContentDto dto,
            CancellationToken cancellationToken
        );

        //Task<PopupsBase> Delete(
        //    DeletePopupTypesDto dto, CancellationToken cancellationToken
        //);
    }
}