using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.SensorType
{
    public class SensorTypeViewModel
    {
        public SensorTypeViewModel(SensorTypeModel model)
        {
            Model = model;
        }
        
        public SensorTypeModel Model { get; set; }
    }
}