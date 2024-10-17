using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Models;

public class SubscriptionsBase: BaseQueryResponseDto
{
    public string? ServiceType { get; set; }
    public string? SubscriptionDate { get; set; }
    public string? SubscriptionEndDate { get; set; }
    public bool? IsActive { get; set; }
    public string? SubscriptionType { get; set; }
    public string? PaymentFrequency { get; set; }
    public int? Quantity { get; set; }
    public string? Response { get; set; }
}

public class ViewSubscriptionsDto: IBaseQueryDto 
{
    public int? CoreClientId { get; set; }
    public int? TerminalId { get; set; }
    public int? Page {  get; set; }
    public int? Size { get; set; }
}

public class CreateSubscriptions {
    public int ClientId { get; set; }
    public string ClientFullName { get; set; }
    public string CardNumber { get; set; }
    public int UserType { get; set; }
    public int SubscriptionPaymentId { get; set; }
    public int? Quantity { get; set; }
}

public class CreateSubscriptionsDto: CreateSubscriptions, IBaseCreateDto {
    public int CreatorId { get; set; }
}