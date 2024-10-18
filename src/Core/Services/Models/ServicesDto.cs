using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Models;

public class ServicesBase: BaseQueryResponseDto
{
    public int? ServiceId { get; set; }
    public string? Description { get; set; }
    public int? DayLimit { get; set; }
    public string? CollectionTime { get; set; }
    public string? PaymentType { get; set; }
    public string? SubscriptionType { get; set; }
    public int? SubscriptionServiceId { get; set; }
    public int? Coverage { get; set; }
    public string? PaymentFrequency { get; set; }
    public int? MonthDuration { get; set; }
    public string? UserType { get; set; } 
    public int? SubscriptionPaymentsId { get; set; }
    public decimal? Amount { get; set; }
    public decimal? TotalAmount { get; set; }
    public int? Duration { get; set; }
    public int? PaymentDay { get; set; }
}

public class ViewServicesDto: IBaseQueryDto 
{
    public int? ServiceId { get; set; }
    public int? TerminalId { get; set; }
    public int? Page {  get; set; }
    public int? Size { get; set; }
}


public class ViewAllServicesDto: IBaseQueryDto
{
    public int? TerminalId { get; set; }
    public int? Page {  get; set; }
    public int? Size { get; set; }

}

public class CreateServicesDto {}