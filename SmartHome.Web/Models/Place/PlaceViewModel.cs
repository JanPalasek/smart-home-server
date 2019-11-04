using SmartHome.Shared.Models;

namespace SmartHome.Web.Models.Place
{
    public class PlaceViewModel : SmartHomeViewModel<PlaceModel>
    {
        public PlaceViewModel(PlaceModel model) : base(model)
        {
        }
    }
}