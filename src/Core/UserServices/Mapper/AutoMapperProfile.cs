using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Queries;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Mapper;

public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListUserServicesQuery, ViewUserServicesDto>();
    }
}