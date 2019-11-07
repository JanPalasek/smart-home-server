using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Sensor
{
    public class SensorViewModel : SmartHomeViewModel<SensorModel>
    {
        public SensorViewModel(
            SensorModel model) : base(model)
        {
        }

        public bool ReadOnly { get; set; }

        public IEnumerable<SelectListItem> BatteryPowerSourceTypes { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> SensorTypes { get; set; } = Enumerable.Empty<SelectListItem>();

        public IEnumerable<SelectListItem> Places { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}