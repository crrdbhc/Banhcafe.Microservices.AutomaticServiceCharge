using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.RenewalTypes.Models;

public class RenewalTypesBase: BaseQueryResponseDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Response { get; set; }
}

public class ViewRenewalTypesDto: IBaseQueryDto 
{
    public int? TerminalId { get; set; }
    public int? Page {  get; set; }
    public int? Size { get; set; }
}

public class CreateRenewalTypesDto {}

public class UpdateUserService { }

public class UpdateRenewalTypesDto: UpdateUserService, IBaseUpdateDto
{
    public int UpdatedId { get; set; }
}

public class DeleteUserService { }
public class DeleteRenewalTypesDto : DeleteUserService, IBaseDeleteDto
{
    public int DeletedId { get; set; }
}