using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Exceptions;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Ports;
using FluentValidation;
using MediatR;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Commands.Create
{
    public sealed class CreateServiceCommand: IRequest<ApiResponse<ServicesBase>>
    {
        public CreateServices Services { get; set; }
    }

    public sealed class ServicesCommandHandler (
        IServicesRepository repository,
        IUserContext userContext,
        IMapper mapper,
        IValidator<CreateServices> _validator
    ) : IRequestHandler<CreateServiceCommand, ApiResponse<ServicesBase>>
    {
        public async Task<ApiResponse<ServicesBase>> Handle (
            CreateServiceCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<ServicesBase>();

            var validationResults = await _validator.ValidateAsync(
                request.Services,
                cancellationToken
            );

            if (!validationResults.IsValid)
            {
                handlerResponse.ValidationErrors = validationResults.ToDictionary();
                return handlerResponse;
            }

            var dto = mapper.Map<CreateServicesDto>(request.Services);
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