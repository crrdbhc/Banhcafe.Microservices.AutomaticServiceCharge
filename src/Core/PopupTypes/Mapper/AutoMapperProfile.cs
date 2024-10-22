using AutoMapper;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Queries;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Mapper;

public sealed class AutoMapperProfile: Profile 
{
    public AutoMapperProfile()
    {
        CreateMap<ListAllPopupTypesQuery, ViewAllPopupTypesDto>();
        CreateMap<CreatePopupType, CreatePopupTypesDto>();
        CreateMap<UpdatePopupType, UpdatePopupTypesDto>();
    }
}