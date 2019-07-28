using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Database.Entities;
using SmartHome.Database.Repositories;
using SmartHome.Shared;

namespace SmartHome.Web.Controllers.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController : Controller
    {
        private readonly ITemperatureMeasurementRepository temperatureMeasurementRepository;
        private readonly IBatteryMeasurementRepository batteryMeasurementRepository;

        public UnitsController(
            ITemperatureMeasurementRepository temperatureMeasurementRepository,
            IBatteryMeasurementRepository batteryMeasurementRepository)
        {
            this.temperatureMeasurementRepository = temperatureMeasurementRepository;
            this.batteryMeasurementRepository = batteryMeasurementRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> Temperature(int unitId, double temperature, double? voltage)
        {
            await temperatureMeasurementRepository.AddAsync(unitId, temperature);
            
            if (voltage != null)
            {
                await batteryMeasurementRepository.AddAsync(unitId, voltage.Value);
            }

            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> Temperatures(int? unitId, DateTime? from, DateTime? to)
        {
            var filter = new MeasurementFilter()
            {
                From = from,
                To = to,
                UnitId = unitId
            };

            var temperatureMeasurements = await temperatureMeasurementRepository.GetTemperatureMeasurements(filter);

            return Json(temperatureMeasurements.Select(x => new { x.Temperature, x.MeasurementDateTime, x.UnitId }).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> LastTemperature(int unitId)
        {
            throw new NotImplementedException();
        }
    }
}