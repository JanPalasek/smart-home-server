using SmartHome.Shared.Models;

namespace SmartHome.Web.Models
{
    public class SmartHomeViewModel<TModel> where TModel : Model
    {
        public TModel Model { get; set; }

        public SmartHomeViewModel(TModel model)
        {
            Model = model;
        }
    }
}