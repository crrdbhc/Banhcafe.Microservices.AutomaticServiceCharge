using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Ports;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Queries;
public sealed class ListPopupsQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<PopupsBase>>>
{
    public int? CoreClientId { get; set; }
}

public sealed class PopupsHandler (IPopupsRepository repository, IMapper mapper, IUserContext userContext) : IRequestHandler<ListPopupsQuery, ApiResponse<IEnumerable<PopupsBase>>>
{
    public async Task<ApiResponse<IEnumerable<PopupsBase>>> Handle(ListPopupsQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<IEnumerable<PopupsBase>>();

        var filtersDto = mapper.Map<ViewPopupsDto>(request);
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
