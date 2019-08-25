using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateUserModel>()
                .ForMember(x => x.Password, x => x.Ignore())
                .ForMember(x => x.Email, x => x.MapFrom(y => y.Email))
                .ReverseMap()
                .ForMember(x => x.EmailConfirmed, x => x.Ignore())
                .ForMember(x => x.LockoutEnabled, x => x.Ignore())
                .ForMember(x => x.LockoutEnd, x => x.Ignore())
                .ForMember(x => x.NormalizedEmail, x => x.Ignore())
                .ForMember(x => x.PasswordHash, x => x.Ignore())
                .ForMember(x => x.PhoneNumber, x => x.Ignore())
                .ForMember(x => x.PhoneNumberConfirmed, x => x.Ignore())
                .ForMember(x => x.SecurityStamp, x => x.Ignore())
                .ForMember(x => x.AccessFailedCount, x => x.Ignore())
                .ForMember(x => x.NormalizedUserName, x => x.Ignore())
                .ForMember(x => x.TwoFactorEnabled, x => x.Ignore())
                .ForMember(x => x.ConcurrencyStamp, x => x.Ignore());
            
            CreateMap<UserModel, User>()
                .ForMember(x => x.EmailConfirmed, x => x.Ignore())
                .ForMember(x => x.LockoutEnabled, x => x.Ignore())
                .ForMember(x => x.LockoutEnd, x => x.Ignore())
                .ForMember(x => x.NormalizedEmail, x => x.Ignore())
                .ForMember(x => x.PhoneNumber, x => x.Ignore())
                .ForMember(x => x.PhoneNumberConfirmed, x => x.Ignore())
                .ForMember(x => x.SecurityStamp, x => x.Ignore())
                .ForMember(x => x.AccessFailedCount, x => x.Ignore())
                .ForMember(x => x.NormalizedUserName, x => x.Ignore())
                .ForMember(x => x.TwoFactorEnabled, x => x.Ignore())
                .ForMember(x => x.ConcurrencyStamp, x => x.Ignore())
                .ReverseMap();
        }
    }
}