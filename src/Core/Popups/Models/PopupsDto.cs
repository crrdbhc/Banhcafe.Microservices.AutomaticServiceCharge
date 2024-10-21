using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Models;

public class PopupsBase: BaseQueryResponseDto
{
    public int? PopupId { get; set; }
    public string? PopupName { get; set; }
    public int? PopupTypeId { get; set; }
    public string? PopupType { get; set; }
    public bool? IsActive { get; set; }
    public string? PopupContentType { get; set; }
    public string? Content { get; set; }
    public int? OrderContent { get; set; }
    public string? Response { get; set; }
}

public class ViewPopupsDto: IBaseQueryDto
{
    public int? PopupId { get; set; }
    public int? TerminalId { get; set; }
    public int? Page {  get; set; }
    public int? Size { get; set; }
}

public class ViewAllPopupsDto: IBaseQueryDto
{
    public int? TerminalId { get; set; }
    public int? Page { get; set; }
    public int? Size { get; set; }
}

public class PopupContent
{
    public int? PopupContentTypeId { get; set; }
    public string? Content { get; set; }
    public int? OrderContent { get; set; }
}

public class PopupData
{
    public string? PopupName { set; get; }
    public int? PopupTypeId {  set; get; }
    public bool? IsActive { get; set; }
    public int? LimitDaysHidden { get; set; }
    public List<PopupContent> PopupContent { get; set; }
}

public class CreatePopup {
    public PopupData? JsonFile { get; set; }
}

public class CreatePopupsDto: CreatePopup, IBaseCreateDto 
{
    public int CreatorId { get; set; }
}