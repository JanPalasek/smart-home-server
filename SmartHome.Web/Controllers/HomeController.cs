using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Configurations;
using SmartHome.Web.Models.Home;
using IConfiguration = AutoMapper.Configuration.IConfiguration;

namespace SmartHome.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITemperatureMeasurementRepository temperatureMeasurementRepository;
        private readonly FileManagerConfiguration fileManagerConfiguration;

        public HomeController(ITemperatureMeasurementRepository temperatureMeasurementRepository,
            FileManagerConfiguration fileManagerConfiguration)
        {
            this.temperatureMeasurementRepository = temperatureMeasurementRepository;
            this.fileManagerConfiguration = fileManagerConfiguration;
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
        
        [Authorize]
        [Route("Files")]
        public IActionResult Files()
        {
            return View("Files", fileManagerConfiguration);
        }
    }
}