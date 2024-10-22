using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Queries;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Mapper;

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