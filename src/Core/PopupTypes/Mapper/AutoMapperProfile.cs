using AutoMapper;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Queries;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Mapper;

public sealed class AutoMapperProfile: Profile 
{
    public AutoMapperProfile()
    {
        CreateMap<ListAllPopupTypesQuery, ViewAllPopupTypesDto>();
        CreateMap<CreatePopupType, CreatePopupTypesDto>();
    }
}