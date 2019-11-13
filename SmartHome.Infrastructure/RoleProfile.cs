using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data;
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

            CreateMap<Role, EntityReference>()
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id));

            CreateMap<CreateRoleModel, Role>(MemberList.Source)
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Name));
        }
    }
}