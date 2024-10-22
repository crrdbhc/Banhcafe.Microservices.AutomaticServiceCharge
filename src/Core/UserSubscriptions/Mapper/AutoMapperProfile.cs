using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Queries;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListUserSubscriptionsQuery, ViewUserSubscriptionsDto>();
        CreateMap<CreateUserSubscriptions, CreateUserSubscriptionsDto>();
    }
}