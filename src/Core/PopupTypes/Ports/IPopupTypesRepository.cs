using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Ports
{
    public interface IPopupTypesRepository: IGenericRepository<PopupTypesBase, ViewAllPopupTypesDto, CreatePopupTypesDto, UpdatePopupTypesDto, DeletePopupTypesDto>
    {
        Task<PopupTypesBase> Create(CreatePopupTypesDto dto, CancellationToken cancellationToken);
        Task<IEnumerable<PopupTypesBase>> ListAll (
            ViewAllPopupTypesDto filtersDto,
            CancellationToken cancellationToken
        );

        Task<PopupTypesBase> Update(
            UpdatePopupTypesDto dto, CancellationToken cancellationToken
        );

        Task<PopupTypesBase> Delete(
            DeletePopupTypesDto dto, CancellationToken cancellationToken
        );
    }
}