using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Exceptions;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Ports;
using FluentValidation;
using MediatR;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Commands.Create
{
    public sealed class CreatePopupCommand : IRequest<ApiResponse<PopupsBase>>
    {
        public CreatePopup Popups { get; set; }
    }

    public sealed class CreatePopupCommandHandler(
        IPopupsRepository repository,
        IUserContext userContext,
        IMapper mapper,
        IValidator<CreatePopup> _validator
    ) : IRequestHandler<CreatePopupCommand, ApiResponse<PopupsBase>>
    {
        public async Task<ApiResponse<PopupsBase>> Handle(
            CreatePopupCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<PopupsBase>();

            var validationResults = await _validator.ValidateAsync(
                request.Popups,
                cancellationToken
            );

            if (!validationResults.IsValid)
            {
                handlerResponse.ValidationErrors = validationResults.ToDictionary();
                return handlerResponse;
            }

            var dto = mapper.Map<CreatePopupsDto>(request.Popups);
            dto.CreatorId = userContext.User.GetUserId();

            try
            {
                var results = await repository.Create(dto, cancellationToken);
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
