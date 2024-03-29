using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Place
{
    public class PlaceListViewModel
    {
        public PlaceListViewModel(IEnumerable<PlaceModel> items)
        {
            Items = items;
        }

        public bool CanCreate { get; set; }
        public IEnumerable<PlaceModel> Items { get; }
    }
}