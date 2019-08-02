using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Database.Entities;
using SmartHome.Database.Repositories;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared;

namespace SmartHome.Web.Controllers.ApiControllers
{
    [ApiController]
    public class TemperaturesController : Controller
    {
        private readonly ITemperatureMeasurementRepository temperatureMeasurementRepository;
        private readonly IBatteryMeasurementRepository batteryMeasurementRepository;
        private readonly IUnitRepository unitRepository;

        public TemperaturesController(
            ITemperatureMeasurementRepository temperatureMeasurementRepository,
            IBatteryMeasurementRepository batteryMeasurementRepository,
            IUnitRepository unitRepository)
        {
            this.temperatureMeasurementRepository = temperatureMeasurementRepository;
            this.batteryMeasurementRepository = batteryMeasurementRepository;
            this.unitRepository = unitRepository;
        }
        
        [HttpPost("api/units/{unitId:int}/temperatures")]
        [HttpPost("api/temperatures")]
        public async Task<IActionResult> Temperature(int unitId, double temperature, double? voltage)
        {
            await temperatureMeasurementRepository.AddAsync(unitId, temperature, DateTime.Now);
            
            // if voltage has been measured and the unit is currently on a battery source => add battery voltage measurement
            if (voltage != null && await unitRepository.AnyWithBatteryPowerSourceAsync(unitId))
            {
                await batteryMeasurementRepository.AddAsync(unitId, voltage.Value, DateTime.Now);
            }

            return Ok();
        }
        
        [HttpGet("api/units/{unitId:int}/temperatures")]
        [HttpGet("api/temperatures")]
        public async Task<IActionResult> Temperatures(int? unitId, DateTime? from, DateTime? to)
        {
            var filter = new MeasurementFilter()
            {
                From = from,
                To = to,
                UnitId = unitId
            };

            var temperatureMeasurements = await temperatureMeasurementRepository.GetTemperatureMeasurementsAsync(filter);

            return Json(temperatureMeasurements.Select(x => new { x.Temperature, x.MeasurementDateTime, x.UnitId }).ToList());
        }

        [HttpGet("api/units/{unitId:int}/temperatures/last")]
        public async Task<IActionResult> LastUnitTemperature(int unitId)
        {
            var temperatureMeasurement = await temperatureMeasurementRepository.GetUnitLastTemperatureMeasurementAsync(unitId);

            if (temperatureMeasurement == null)
            {
                return BadRequest("Given unit does not exist or doesn't have temperature measurements.");
            }

            return Json(temperatureMeasurement.Temperature);
        }
    }
}