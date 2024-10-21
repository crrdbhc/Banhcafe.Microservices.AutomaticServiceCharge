using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Extensions;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Currencies.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Currencies.Ports;
using MediatR;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Currencies.Queries;

public sealed class ListCurrenciesQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<CurrenciesBase>>>
{
}

public sealed class CurrenciesHandler (ICurrenciesRepository repository, IMapper mapper, IUserContext userContext) : IRequestHandler<ListCurrenciesQuery, ApiResponse<IEnumerable<CurrenciesBase>>>
{
    public async Task<ApiResponse<IEnumerable<CurrenciesBase>>> Handle(ListCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<IEnumerable<CurrenciesBase>>();

        var filtersDto = mapper.Map<ViewCurrenciesDto>(request);
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