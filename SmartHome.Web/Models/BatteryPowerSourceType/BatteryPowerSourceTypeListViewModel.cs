using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.BatteryPowerSourceType
{
    public class BatteryPowerSourceTypeListViewModel
    {
        public BatteryPowerSourceTypeListViewModel(IEnumerable<BatteryPowerSourceTypeModel> items)
        {
            Items = items;
        }

        public bool CanCreate { get; set; }
        public IEnumerable<BatteryPowerSourceTypeModel> Items { get; }
    }
}