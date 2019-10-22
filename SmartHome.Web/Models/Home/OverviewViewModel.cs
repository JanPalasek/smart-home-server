using System.Collections;
using System.Collections.Generic;
using SmartHome.Shared.Models;

namespace SmartHome.Web.Models.Home
{
    public class OverviewViewModel
    {
        /// <summary>
        /// Last temperature measurements for each sensor.
        /// </summary>
        public IList<OverviewTemperatureMeasurementModel>? LastSensorTemperatureMeasurements { get; set; }
    }
}