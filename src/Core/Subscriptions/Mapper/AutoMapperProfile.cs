using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Queries;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListSubscriptionsQuery, ViewSubscriptionsDto>();
        CreateMap<CreateSubscriptions, CreateSubscriptionsDto>();
    }
}