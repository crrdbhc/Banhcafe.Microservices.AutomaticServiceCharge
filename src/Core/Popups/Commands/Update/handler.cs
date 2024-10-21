using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Exceptions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Ports;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Commands.Update
{
    public sealed class UpdatePopupsCommand : IRequest<ApiResponse<PopupsBase>>
    {
        public UpdatePopup Popups {  get; set; }
    }

    public sealed class PopupsCommandHandler (
        IPopupsRepository repository,
        IUserContext userContext,
        IMapper mapper
    ) : IRequestHandler<UpdatePopupsCommand, ApiResponse<PopupsBase>>
    {
        public async Task<ApiResponse<PopupsBase>> Handle (
            UpdatePopupsCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<PopupsBase>();
            var dto = mapper.Map<UpdatePopupsDto>(request.Popups);
            dto.UpdatedId = userContext.User.GetUserId();

            try
            {
                var results = await repository.Update(dto, cancellationToken);
                handlerResponse.Data = results;
            }
            catch(ServiceException ex)
            {
                handlerResponse.AddErrors(ex.Message);
            }

            return handlerResponse;
        }
    }

    public sealed class UpdatePopupContentCommand : IRequest<ApiResponse<PopupsBase>>
    {
        public UpdatePopupContent PopupContent { get; set; }
    }

    public sealed class PopupsContentCommandHandler(
        IPopupsRepository repository,
        IUserContext userContext,
        IMapper mapper
    ) : IRequestHandler<UpdatePopupContentCommand, ApiResponse<PopupsBase>>
    {
        public async Task<ApiResponse<PopupsBase>> Handle(
            UpdatePopupContentCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<PopupsBase>();
            var dto = mapper.Map<UpdatePopupContentDto>(request.PopupContent);
            dto.UpdatedId = userContext.User.GetUserId();

            try
            {
                var results = await repository.Update(dto, cancellationToken);
                handlerResponse.Data = results;
            }
            catch (ServiceException ex)
            {
                handlerResponse.AddErrors(ex.Message);
            }

            return handlerResponse;
        }
    }
}
