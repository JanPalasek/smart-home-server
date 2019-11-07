using System.Collections;
using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

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