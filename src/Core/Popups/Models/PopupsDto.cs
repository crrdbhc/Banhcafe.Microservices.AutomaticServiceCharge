using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Models;

public class PopupsBase: BaseQueryResponseDto
{
    public int? PopupId { get; set; }
    public string? PopupName { get; set; }
    public string? PopupType { get; set; }
    public bool? IsActive { get; set; }
    public string Content { get; set; }
    public int OrderContent { get; set; }
    public string? PopupContentTypeName { get; set; }
}

public class ViewPopupsDto: IBaseQueryDto
{
    public int? CoreClientId { get; set; }
    public int? TerminalId { get; set; }
    public int? Page {  get; set; }
    public int? Size { get; set; }
}

public class CreatePopupsDto { }