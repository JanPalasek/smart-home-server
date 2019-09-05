using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Repositories.Interfaces;
using SmartHome.Web.Models.Home;

namespace SmartHome.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITemperatureMeasurementRepository temperatureMeasurementRepository;

        public HomeController(ITemperatureMeasurementRepository temperatureMeasurementRepository)
        {
            this.temperatureMeasurementRepository = temperatureMeasurementRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Overview()
        {
            var temperatureMeasurements = await temperatureMeasurementRepository.GetAllSensorsLastTemperatureMeasurementAsync();
            var vm = new OverviewViewModel()
            {
                LastSensorTemperatureMeasurements = temperatureMeasurements
            };
            
            return View("Overview", vm);
        }
    }
}