using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Currencies.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Currencies.Queries;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Currencies.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListCurrenciesQuery, ViewCurrenciesDto>();
    }
}