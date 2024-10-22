using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Ports;
using MediatR;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Queries;

public sealed class ListUserServicesQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<UserServicesBase>>>
{
    public int? CoreClientId { get; set; }
    public int? ServiceId { get; set; }
}

public sealed class UserServicesHandler (IUserServicesRepository repository, IMapper mapper, IUserContext userContext) : IRequestHandler<ListUserServicesQuery, ApiResponse<IEnumerable<UserServicesBase>>>
{
    public async Task<ApiResponse<IEnumerable<UserServicesBase>>> Handle(ListUserServicesQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<IEnumerable<UserServicesBase>>();

        var filtersDto = mapper.Map<ViewUserServicesDto>(request);
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