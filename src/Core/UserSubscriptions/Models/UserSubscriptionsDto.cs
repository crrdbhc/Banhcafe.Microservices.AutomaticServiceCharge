using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserSubscriptions.Models;

public class UserSubscriptionsBase: BaseQueryResponseDto
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

public class ViewUserSubscriptionsDto: IBaseQueryDto 
{
    public int? CoreClientId { get; set; }
    public int? TerminalId { get; set; }
    public int? Page {  get; set; }
    public int? Size { get; set; }
}

public class CreateUserSubscriptions {
    public int ClientId { get; set; }
    public string ClientFullName { get; set; }
    public string CardNumber { get; set; }
    public int UserType { get; set; }
    public int SubscriptionPaymentId { get; set; }
    public int? Quantity { get; set; }
}

public class CreateUserSubscriptionsDto: CreateUserSubscriptions, IBaseCreateDto {
    public int CreatorId { get; set; }
}