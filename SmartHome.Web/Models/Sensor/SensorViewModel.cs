using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Sensor
{
    public class SensorViewModel
    {
        public SensorViewModel(
            SensorModel model)
        {
            Model = model;
        }

        public bool IsCreatePage { get; set; }
        
        public SensorModel Model { get; set; }

        public IEnumerable<BatteryPowerSourceTypeModel> BatteryPowerSourceTypes { get; set; } = Enumerable.Empty<BatteryPowerSourceTypeModel>();
        public IEnumerable<SensorTypeModel> SensorTypes { get; set; } = Enumerable.Empty<SensorTypeModel>();

        public IEnumerable<PlaceModel> Places { get; set; } = Enumerable.Empty<PlaceModel>();
    }
}