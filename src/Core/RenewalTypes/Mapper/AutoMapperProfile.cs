using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.RenewalTypes.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.RenewalTypes.Queries;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.RenewalTypes.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListRenewalTypesQuery, ViewRenewalTypesDto>();
    }
}