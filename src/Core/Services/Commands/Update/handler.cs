using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Exceptions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Ports;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Commands.Update
{
    public sealed class UpdateServicesCommand : IRequest<ApiResponse<ServicesBase>>
    {
        public UpdateService Services { get; set; }
    }

    public sealed class ServicesCommandHandler (
        IServicesRepository repository,
        IUserContext userContext,
        IMapper mapper
    ) : IRequestHandler<UpdateServicesCommand, ApiResponse<ServicesBase>>
    {
        public async Task<ApiResponse<ServicesBase>> Handle (
            UpdateServicesCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<ServicesBase>();
            var dto = mapper.Map<UpdateServicesDto>(request.Services);
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

    public sealed class UpdateServicesSubscriptionsCommand : IRequest<ApiResponse<ServicesBase>>
    {
        public UpdateServicesSubscriptions Services {  get; set; }
    }

    public sealed class ServiceSubscriptionsCommandHandler (
        IServicesRepository repository,
        IUserContext userContext,
        IMapper mapper
    ) : IRequestHandler<UpdateServicesSubscriptionsCommand, ApiResponse<ServicesBase>>
    {
        public async Task<ApiResponse<ServicesBase>> Handle(
            UpdateServicesSubscriptionsCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<ServicesBase>();
            var dto = mapper.Map<UpdateServiceSubscriptionsDto>(request.Services);
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

    public sealed class UpdateSubscriptionPaymentsCommand : IRequest<ApiResponse<ServicesBase>>
    {
        public UpdateSubscriptionsPayments Services { get; set; }
    }

    public sealed class SubscriptionsPaymentsCommandHandler (
        IServicesRepository repository,
        IUserContext userContext,
        IMapper mapper
    ) : IRequestHandler<UpdateSubscriptionPaymentsCommand, ApiResponse<ServicesBase>>
    {
        public async Task<ApiResponse<ServicesBase>> Handle (
            UpdateSubscriptionPaymentsCommand request,
            CancellationToken cancellationToken
        )
        {
            var handlerResponse = new ApiResponse<ServicesBase>();
            var dto = mapper.Map<UpdateSubscriptionsPaymentsDto>(request.Services);
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
