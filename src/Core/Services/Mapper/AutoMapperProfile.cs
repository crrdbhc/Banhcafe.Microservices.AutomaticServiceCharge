using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Queries;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListServicesQuery, ViewServicesDto>();
        CreateMap<ListAllServicesQuery, ViewAllServicesDto>();
        CreateMap<CreateServices, CreateServicesDto>();
        CreateMap<UpdateService, UpdateServicesDto>();
        CreateMap<UpdateServicesSubscriptions, UpdateServiceSubscriptionsDto>();
        CreateMap<UpdateSubscriptionsPayments, UpdateSubscriptionsPaymentsDto>();
    }
}