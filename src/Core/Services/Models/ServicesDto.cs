using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;
using MediatR.NotificationPublishers;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Models;

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
    public string? Response { get; set; }
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

public class PaymentFrequencies 
{
    public int? PaymentFrequencyId { get; set; }
    public int? UserTypeId { get; set; }
    public decimal? Amount { get; set; }
    public decimal? TotalAmount { get; set; }
    public int? Duration { get; set; }
}

public class Subscriptions 
{
    public int? SubscriptionId { get; set; }
    public int? Coverage { get; set; }
    public List<PaymentFrequencies> PaymentFrequencies { get; set; }

}

public class ServiceData {
    public string? ServiceName { get; set; }
    public string? ServiceDescription { get; set; }
    public int? PaymentTypeId { get; set; }  
    public int? DayLimit { get; set; }
    public int? PaymentDay { get; set; }
    public string? CollectionTime { get; set; }
    public List<Subscriptions> Subscriptions { get; set; } 
}

public class CreateServices {
    public ServiceData? JsonFile { get; set; }
}

public class CreateServicesDto : CreateServices, IBaseCreateDto {
    public int CreatorId { get; set; }
}

public class UpdateService 
{
    public int ServiceId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? DayLimit { get; set; }
    public int? PaymentTypeId { get; set; }
    public string? CollectionTime { get; set; }
    public int? CurrencyTypeId { get; set; }
    public int? RenewalTypeId { get; set; }
    public int? PaymentDay { get; set; }

}

public class UpdateServicesDto : UpdateService, IBaseUpdateDto
{
    public int UpdatedId { get; set; }
}


public class UpdateServicesSubscriptions
{
    public int SubscriptionServiceId { get; set; }
    public int? Coverage { get; set; }
}

public class UpdateServiceSubscriptionsDto : UpdateServicesSubscriptions, IBaseUpdateDto
{
    public int UpdatedId { get; set; }
}

public class UpdateSubscriptionsPayments
{
    public int SubscriptionPaymentId { get; set; }
    public int SubscriptionServiceId { get; set; }
    public int? PaymentFrequencyId { get; set; }
    public int? UserTypeId { get; set; }
    public decimal? Amount { get; set; }
    public decimal? TotalAmount { get; set; }
    public int? Duration { get; set; }
}

public class UpdateSubscriptionsPaymentsDto : UpdateSubscriptionsPayments, IBaseUpdateDto
{
    public int UpdatedId { get; set; }
}

public class DeleteService { }
public class DeleteServicessDto : DeleteService, IBaseDeleteDto
{
    public int DeletedId { get; set; }
}