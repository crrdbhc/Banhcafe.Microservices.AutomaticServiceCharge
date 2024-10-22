using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Exceptions;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Ports;
using FluentValidation;
using MediatR;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Commands.Create
{
    public sealed class CreateUserSubscriptionCommand: IRequest<ApiResponse<UserSubscriptionsBase>>
    {
        public CreateUserSubscriptions Subscriptions {get; set;}
    }
    
    public sealed class UserSubscriptionsCommandHandler (
        IUserSubscriptionsRepository repository,
        IUserContext userContext,
        IMapper mapper,
        IValidator<CreateUserSubscriptions> _validator
    ) : IRequestHandler<CreateUserSubscriptionCommand, ApiResponse<UserSubscriptionsBase>>
    {
        public async Task<ApiResponse<UserSubscriptionsBase>> Handle (
            CreateUserSubscriptionCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<UserSubscriptionsBase>();

            var validationResults = await _validator.ValidateAsync(
                request.Subscriptions,
                cancellationToken
            );

            if (!validationResults.IsValid)
            {
                handlerResponse.ValidationErrors = validationResults.ToDictionary();
                return handlerResponse;
            }

            var dto = mapper.Map<CreateUserSubscriptionsDto>(request.Subscriptions);
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