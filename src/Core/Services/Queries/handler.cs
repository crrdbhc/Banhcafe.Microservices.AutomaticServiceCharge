using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Ports;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Queries;

public sealed class ListServicesQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<ServicesBase>>>
{
    public int? CoreClientId { get; set; }
    public int? ServiceId { get; set; }
}

public sealed class ServicesHandler (IServicesRepository repository, IMapper mapper, IUserContext userContext) : IRequestHandler<ListServicesQuery, ApiResponse<IEnumerable<ServicesBase>>>
{
    public async Task<ApiResponse<IEnumerable<ServicesBase>>> Handle(ListServicesQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<IEnumerable<ServicesBase>>();

        var filtersDto = mapper.Map<ViewServicesDto>(request);
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