using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<Place, PlaceModel>().ReverseMap();
        }
    }
}