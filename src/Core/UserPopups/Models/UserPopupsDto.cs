using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Models;

public class UserPopupsBase: BaseQueryResponseDto
{
    public int? PopupId { get; set; }
    public string? PopupName { get; set; }
    public string? PopupType { get; set; }
    public bool? IsActive { get; set; }
    public string Content { get; set; }
    public int OrderContent { get; set; }
    public string? PopupContentTypeName { get; set; }
    public string? Response { get; set; }
}

public class ViewUserPopupsDto: IBaseQueryDto
{
    public int? CoreClientId { get; set; }
    public int? TerminalId { get; set; }
    public int? Page {  get; set; }
    public int? Size { get; set; }
}

public class HideUserPopup
{
    public int ClientId { get; set; }
    public int PopupId { get; set; }
}

public class HideUserPopupDto: HideUserPopup, IBaseCreateDto 
{
    public int CreatorId { get; set; }
}

public class UpdateUserPopup { }

public class UpdateUserPopupsDto : UpdateUserPopup, IBaseUpdateDto
{
    public int UpdatedId { get; set; }
}

public class DeleteUserPopup { }
public class DeleteUserPopupsDto : DeleteUserPopup, IBaseDeleteDto
{
    public int DeletedId { get; set; }
}