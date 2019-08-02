using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Shared;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class UnitTypeProfile : Profile
    {
        public UnitTypeProfile()
        {
            CreateMap<UnitType, UnitTypeModel>().ReverseMap();
            CreateMap<UnitType, EntityReference>();
        }
    }
}