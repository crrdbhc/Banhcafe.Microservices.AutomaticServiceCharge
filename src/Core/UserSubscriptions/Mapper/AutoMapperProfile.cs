using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserSubscriptions.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserSubscriptions.Queries;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserSubscriptions.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListUserSubscriptionsQuery, ViewUserSubscriptionsDto>();
        CreateMap<CreateUserSubscriptions, CreateUserSubscriptionsDto>();
    }
}