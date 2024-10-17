using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Ports;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Queries;

public sealed class ListSubscriptionsQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<SubscriptionsBase>>>
{
    public int? CoreClientId { get; set; }
}

public sealed class SubscriptionsHandler (ISubscriptionsRepository repository, IMapper mapper, IUserContext userContext) : IRequestHandler<ListSubscriptionsQuery, ApiResponse<IEnumerable<SubscriptionsBase>>>
{
    public async Task<ApiResponse<IEnumerable<SubscriptionsBase>>> Handle(ListSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<IEnumerable<SubscriptionsBase>>();

        var filtersDto = mapper.Map<ViewSubscriptionsDto>(request);
        filtersDto.TerminalId = userContext.User.GetTerminalId();

        var results = await repository.List(filtersDto, cancellationToken);

        if (results.Any())
        {
            BaseQueryResponseDto pagination = results.FirstOrDefault();

            if (pagination is null || !pagination.Success)
            {
                apiResponse.AddErrors(pagination?.Message ?? "Ocurrio un error.");
                return apiResponse;
            }

            apiResponse.AddPagination(pagination);
            apiResponse.Data = results;
        }

        return apiResponse;
    }
}