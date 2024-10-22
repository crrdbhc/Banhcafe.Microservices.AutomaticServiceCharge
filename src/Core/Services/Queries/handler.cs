using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Ports;
using MediatR;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Queries;

public sealed class ListServicesQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<ServicesBase>>>
{
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

public sealed class ListAllServicesQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<ServicesBase>>>
{
}

public sealed class AllServicesHandler (IServicesRepository repository, IMapper mapper, IUserContext userContext) : IRequestHandler<ListAllServicesQuery, ApiResponse<IEnumerable<ServicesBase>>>
{
    public async Task<ApiResponse<IEnumerable<ServicesBase>>> Handle(ListAllServicesQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<IEnumerable<ServicesBase>>();

        var filtersDto = mapper.Map<ViewAllServicesDto>(request);
        filtersDto.TerminalId = userContext.User.GetTerminalId();

        var results = await repository.ListAll(filtersDto, cancellationToken);

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