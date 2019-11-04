using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHome.Shared.Models;

namespace SmartHome.Web.Models.SensorType
{
    public class SensorTypeViewModel : SmartHomeViewModel<SensorTypeModel>
    {
        public SensorTypeViewModel(SensorTypeModel model) : base(model)
        {
        }
    }
}