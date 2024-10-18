using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Ports
{
    public interface IPopupTypesRepository: IGenericRepository<PopupTypesBase, ViewAllPopupTypesDto, CreatePopupTypesDto>
    {
        Task<PopupTypesBase> Create(CreatePopupTypesDto dto, CancellationToken cancellationToken);
        Task<IEnumerable<PopupTypesBase>> ListAll (
            ViewAllPopupTypesDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}