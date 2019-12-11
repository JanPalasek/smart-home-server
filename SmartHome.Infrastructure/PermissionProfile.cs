using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Infrastructure
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionModel>().ReverseMap();
            CreateMap<UserPermission, PermissionRoleModel>()
                .ForMember(x => x.Inherited, x => x.Ignore())
                .ForMember(x => x.PermissionId, x => x.MapFrom(y => y.PermissionId))
                .ForMember(x => x.PermissionName, x => x.MapFrom(y => y.Permission.Name))
                .ForMember(x => x.RoleName, x => x.Ignore());
            CreateMap<RolePermission, PermissionRoleModel>()
                .ForMember(x => x.Inherited, x => x.Ignore())
                .ForMember(x => x.PermissionId, x => x.MapFrom(y => y.PermissionId))
                .ForMember(x => x.PermissionName, x => x.MapFrom(y => y.Permission.Name))
                .ForMember(x => x.RoleName, x => x.MapFrom(y => y.Role.Name));
        }
    }
}