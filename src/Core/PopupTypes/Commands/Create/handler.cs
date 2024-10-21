using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Exceptions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Ports;
using FluentValidation;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Commands.Create
{
    public sealed class CreatePopupTypeCommand: IRequest<ApiResponse<PopupTypesBase>>
    {
        public CreatePopupType PopupTypes { get; set; }
    }

    public sealed class CreatePopupTypeCommandHandler (
        IPopupTypesRepository repository,
        IUserContext userContext,
        IMapper mapper,
        IValidator<CreatePopupType> _validator
    ) : IRequestHandler<CreatePopupTypeCommand, ApiResponse<PopupTypesBase>>
    {
        public async Task<ApiResponse<PopupTypesBase>> Handle (
            CreatePopupTypeCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<PopupTypesBase>();

            var validationResults = await _validator.ValidateAsync(
                request.PopupTypes,
                cancellationToken
            );

            if (!validationResults.IsValid)
            {
                handlerResponse.ValidationErrors = validationResults.ToDictionary();
                return handlerResponse;
            }

            var dto = mapper.Map<CreatePopupTypesDto>(request.PopupTypes);
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

            return handlerResponse;
        }
    }
}