using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Extensions;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Ports;
using MediatR;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Queries;

public sealed class ListAllPopupTypesQuery: BaseQuery, IRequest<ApiResponse<IEnumerable<PopupTypesBase>>>
{

}

public sealed class AllPopupTypesHandler (IPopupTypesRepository repository, IMapper mapper, IUserContext userContext) : IRequestHandler<ListAllPopupTypesQuery, ApiResponse<IEnumerable<PopupTypesBase>>>
{
    public async Task<ApiResponse<IEnumerable<PopupTypesBase>>> Handle(ListAllPopupTypesQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<IEnumerable<PopupTypesBase>>();

        var filtersDto = mapper.Map<ViewAllPopupTypesDto>(request);
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