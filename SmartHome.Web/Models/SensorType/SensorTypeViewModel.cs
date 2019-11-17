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
        
        public bool IsCreatePage { get; set; }
        public bool CanEdit { get; set; }
        
        public SensorTypeModel Model { get; set; }
    }
}