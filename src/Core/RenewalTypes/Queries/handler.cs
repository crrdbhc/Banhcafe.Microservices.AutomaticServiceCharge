using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.RenewalTypes.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.RenewalTypes.Ports;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.RenewalTypes.Queries;

public sealed class ListRenewalTypesQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<RenewalTypesBase>>>
{
}

public sealed class RenewalTypesHandler (IRenewalTypesRepository repository, IMapper mapper, IUserContext userContext) : IRequestHandler<ListRenewalTypesQuery, ApiResponse<IEnumerable<RenewalTypesBase>>>
{
    public async Task<ApiResponse<IEnumerable<RenewalTypesBase>>> Handle(ListRenewalTypesQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<IEnumerable<RenewalTypesBase>>();

        var filtersDto = mapper.Map<ViewRenewalTypesDto>(request);
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