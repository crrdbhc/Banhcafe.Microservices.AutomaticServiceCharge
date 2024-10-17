using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Ports;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Queries;
public sealed class ListUserPopupsQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<UserPopupsBase>>>
{
    public int? CoreClientId { get; set; }
}

public sealed class UserPopupsHandler (IUserPopupsRepository repository, IMapper mapper, IUserContext userContext) : IRequestHandler<ListUserPopupsQuery, ApiResponse<IEnumerable<UserPopupsBase>>>
{
    public async Task<ApiResponse<IEnumerable<UserPopupsBase>>> Handle(ListUserPopupsQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<IEnumerable<UserPopupsBase>>();

        var filtersDto = mapper.Map<ViewUserPopupsDto>(request);
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
