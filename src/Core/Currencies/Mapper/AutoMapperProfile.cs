using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Currencies.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Currencies.Queries;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Currencies.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListCurrenciesQuery, ViewCurrenciesDto>();
    }
}