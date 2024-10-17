using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserSubscriptions.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserSubscriptions.Ports;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserSubscriptions.Queries;

public sealed class ListUserSubscriptionsQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<UserSubscriptionsBase>>>
{
    public int? CoreClientId { get; set; }
}

public sealed class UserSubscriptionsHandler (IUserSubscriptionsRepository repository, IMapper mapper, IUserContext userContext) : IRequestHandler<ListUserSubscriptionsQuery, ApiResponse<IEnumerable<UserSubscriptionsBase>>>
{
    public async Task<ApiResponse<IEnumerable<UserSubscriptionsBase>>> Handle(ListUserSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<IEnumerable<UserSubscriptionsBase>>();

        var filtersDto = mapper.Map<ViewUserSubscriptionsDto>(request);
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