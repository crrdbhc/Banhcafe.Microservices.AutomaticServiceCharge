using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Models;

public class ServicesBase: BaseQueryResponseDto
{
    public string? ServiceName { get; set; }
    public string? ServiceDescription { get; set; }
    public int? DayLimit { get; set; }
    public string? SubscriptionType { get; set; }
    public string? PaymentFrequency { get; set; }
    public string? Currency { get; set; }
    public decimal? Amount { get; set; }
    public int? Coverage { get; set; }
    public int? Duration { get; set; }
    public decimal? TotalAmount { get; set; }
    public int? SubscriptionPaymentId { get; set; } 
    public string? Response { get; set; }
}

public class ViewServicesDto: IBaseQueryDto 
{
    public int? CoreClientId { get; set; }
    public int? ServiceId { get; set; }
    public int? TerminalId { get; set; }
    public int? Page {  get; set; }
    public int? Size { get; set; }
}

public class CreateServicesDto {}