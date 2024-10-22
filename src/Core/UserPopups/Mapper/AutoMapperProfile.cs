using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Queries;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Mapper;
public sealed class AutoMapperProfile: Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ListUserPopupsQuery, ViewUserPopupsDto>();
        CreateMap<HideUserPopup, HideUserPopupDto>();
    }
}
