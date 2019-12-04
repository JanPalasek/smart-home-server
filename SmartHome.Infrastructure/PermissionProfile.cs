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
        }
    }
}