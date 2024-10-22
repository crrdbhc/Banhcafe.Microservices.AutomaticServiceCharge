using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Models;

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

public class UpdatePopupType
{
    public int? PopupTypeId { get; set; }
    public string? PopupTypeName { get; set; }
}

public class UpdatePopupTypesDto : UpdatePopupType, IBaseUpdateDto
{
    public int UpdatedId { get; set; }
}

public class DeletePopupType { }

public class DeletePopupTypesDto : DeletePopupType, IBaseDeleteDto
{
    public int DeletedId { get; set; }
}