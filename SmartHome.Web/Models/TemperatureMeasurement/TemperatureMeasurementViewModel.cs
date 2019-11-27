using System.Collections.Generic;
using System.Linq;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.TemperatureMeasurement
{
    public class TemperatureMeasurementViewModel
    {
        public TemperatureMeasurementViewModel(TemperatureMeasurementModel model)
        {
            Model = model;
        }

        public bool IsCreatePage { get; set; }
        public bool CanEdit { get; set; }
        public IEnumerable<SensorModel> Sensors { get; set; } = Enumerable.Empty<SensorModel>();

        public IEnumerable<PlaceModel> Places { get; set; } = Enumerable.Empty<PlaceModel>();
        public TemperatureMeasurementModel Model { get; set; }
    }
}