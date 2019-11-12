using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Infrastructure
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleModel, Role>()
                .ForMember(x => x.NormalizedName, o => o.Ignore())
                .ForMember(x => x.ConcurrencyStamp, o => o.Ignore())
                .ReverseMap();
        }
    }
}