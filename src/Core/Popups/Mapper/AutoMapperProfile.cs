using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Queries;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Mapper;

public sealed class AutoMapperProfile: Profile 
{
    public AutoMapperProfile()
    {
        CreateMap<ListPopupsQuery, ViewPopupsDto>();
        CreateMap<ListAllPopupsQuery, ViewAllPopupsDto>();
        CreateMap<CreatePopup, CreatePopupsDto>();
        CreateMap<UpdatePopup, UpdatePopupsDto>();
        CreateMap<UpdatePopupContent, UpdatePopupContentDto>();
    }
}