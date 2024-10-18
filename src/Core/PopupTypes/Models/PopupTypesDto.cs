using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Models;

public class PopupTypesBase: BaseQueryResponseDto
{
    public int? PopupTypeId { get; set;}
    public string? PopupTypeName { get; set; }
    public string? Response { get; set; }
}

public class ViewAllPopupTypesDto: IBaseQueryDto
{
    public int? TerminalId { get; set; }
    public int? Page { get; set; }
    public int? Size { get; set; }
}

public class CreatePopupType 
{
    public string? PopupTypeName { get; set; } 
}

public class CreatePopupTypesDto: CreatePopupType, IBaseCreateDto
{
    public int CreatorId { get; set; }
}