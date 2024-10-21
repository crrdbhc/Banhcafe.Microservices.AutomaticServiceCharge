using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Currencies.Models;

public class CurrenciesBase: BaseQueryResponseDto
{
    public int? Id { get; set; }
    public string? Type { get; set; }
    public string? Acronym { get; set; }
    public string? Response { get; set; }
}

public class ViewCurrenciesDto: IBaseQueryDto 
{
    public int? TerminalId { get; set; }
    public int? Page {  get; set; }
    public int? Size { get; set; }
}

public class CreateCurrenciesDto {}

public class UpdateUserService { }

public class UpdateCurrenciesDto: UpdateUserService, IBaseUpdateDto
{
    public int UpdatedId { get; set; }
}

public class DeleteUserService { }
public class DeleteCurrenciesDto : DeleteUserService, IBaseDeleteDto
{
    public int DeletedId { get; set; }
}