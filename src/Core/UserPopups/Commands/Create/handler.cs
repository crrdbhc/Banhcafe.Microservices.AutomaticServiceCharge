using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Exceptions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Ports;
using FluentValidation;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Command.Create 
{
    public sealed class HideUserPopupCommand: IRequest<ApiResponse<UserPopupsBase>>
    {
        public HideUserPopup UserPopups { get; set; }
    }

    public sealed class HideUserPopupCommandHandler (
        IUserPopupsRepository repository,
        IUserContext userContext,
        IMapper mapper,
        IValidator<HideUserPopup> _validator
    ) : IRequestHandler<HideUserPopupCommand, ApiResponse<UserPopupsBase>>
    {
        public async Task<ApiResponse<UserPopupsBase>> Handle (
            HideUserPopupCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<UserPopupsBase>();

            var validationResults = await _validator.ValidateAsync(
                request.UserPopups,
                cancellationToken
            );

            if (!validationResults.IsValid)
            {
                handlerResponse.ValidationErrors = validationResults.ToDictionary();
                return handlerResponse;
            }

            var dto = mapper.Map<HideUserPopupDto>(request.UserPopups);
            dto.CreatorId = userContext.User.GetUserId();

            try
            {
                var results = await repository.Create(dto, cancellationToken);
                handlerResponse.Data = results;
            }
            catch(ServiceException ex)
            {
                handlerResponse.AddErrors(ex.Message);
            }

            // Devolver una respuesta inmediata al cliente
            return handlerResponse;
        }
    }
}