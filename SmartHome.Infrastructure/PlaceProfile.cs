using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Infrastructure
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<Place, PlaceModel>().ReverseMap();
        }
    }
}