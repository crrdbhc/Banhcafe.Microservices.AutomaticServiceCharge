using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Queries;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListServicesQuery, ViewServicesDto>();
    }
}