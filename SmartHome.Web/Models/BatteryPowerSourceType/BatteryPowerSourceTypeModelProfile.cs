using AutoMapper;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.BatteryPowerSourceType
{
    public class BatteryPowerSourceTypeModelProfile : Profile
    {
        public BatteryPowerSourceTypeModelProfile()
        {
            CreateMap<BatteryPowerSourceTypeModel, BatteryPowerSourceTypeGridItemModel>()
                .ForMember(x => x.BatteryTypeName,
                    x => x.MapFrom(y => y.BatteryType.ToString()));
        }
    }
}