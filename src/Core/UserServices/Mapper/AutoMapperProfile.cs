using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserServices.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserServices.Queries;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserServices.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListUserServicesQuery, ViewUserServicesDto>();
    }
}