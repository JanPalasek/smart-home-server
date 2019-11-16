using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Place
{
    public class PlaceViewModel
    {
        public bool CanEdit { get; set; }
        
        public PlaceModel Model { get; set; }
        
        public PlaceViewModel(PlaceModel model)
        {
            this.Model = model;
        }
    }
}