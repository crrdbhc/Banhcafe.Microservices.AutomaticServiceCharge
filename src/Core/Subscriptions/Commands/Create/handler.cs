using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Exceptions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Ports;
using FluentValidation;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Commands.Create
{
    public sealed class CreateSubscriptionCommand: IRequest<ApiResponse<SubscriptionsBase>>
    {
        public CreateSubscriptions Subscriptions { get; set; }
    }
    
    public sealed class SubscriptionsCommandHandler (
        ISubscriptionsRepository repository,
        IUserContext userContext,
        IMapper mapper,
        IValidator<CreateSubscriptions> _validator
    ) : IRequestHandler<CreateSubscriptionCommand, ApiResponse<SubscriptionsBase>>
    {
        public async Task<ApiResponse<SubscriptionsBase>> Handle (
            CreateSubscriptionCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<SubscriptionsBase>();

            var validationResults = await _validator.ValidateAsync(
                request.Subscriptions,
                cancellationToken
            );

            if (!validationResults.IsValid)
            {
                handlerResponse.ValidationErrors = validationResults.ToDictionary();
                return handlerResponse;
            }

            var dto = mapper.Map<CreateSubscriptionsDto>(request.Subscriptions);
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