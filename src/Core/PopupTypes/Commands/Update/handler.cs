
using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Exceptions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Ports;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Commands.Update
{
    public sealed class UpdatePopupTypesCommand : IRequest<ApiResponse<PopupTypesBase>>
    {
        public UpdatePopupType PopupTypes { get; set; }
    }

    public sealed class PopupTypesCommandHandler (
        IPopupTypesRepository repository,
        IUserContext userContext,
        IMapper mapper
    ) : IRequestHandler<UpdatePopupTypesCommand, ApiResponse<PopupTypesBase>>
    {
        public async Task<ApiResponse<PopupTypesBase>> Handle (
            UpdatePopupTypesCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<PopupTypesBase>();
            var dto = mapper.Map<UpdatePopupTypesDto>(request.PopupTypes);
            dto.UpdatedId = userContext.User.GetUserId();

            try
            {
                var results = await repository.Update(dto, cancellationToken);
                handlerResponse.Data = results;
            }
            catch ( ServiceException ex)
            {
                handlerResponse.AddErrors(ex.Message);
            }

            return handlerResponse;
        }
    }
}
