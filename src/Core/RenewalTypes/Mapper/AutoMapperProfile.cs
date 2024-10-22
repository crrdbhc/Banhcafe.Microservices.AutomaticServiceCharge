using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.RenewalTypes.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.RenewalTypes.Queries;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.RenewalTypes.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListRenewalTypesQuery, ViewRenewalTypesDto>();
    }
}