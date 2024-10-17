using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Queries;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserPopups.Mapper;
public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListUserPopupsQuery, ViewUserPopupsDto>();
        CreateMap<HideUserPopup, HideUserPopupDto>();
    }
}
