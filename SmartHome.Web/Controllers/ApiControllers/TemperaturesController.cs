using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.BatteryMeasurement;
using SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement;

namespace SmartHome.Web.Controllers.ApiControllers
{
    public class TemperaturesController : ControllerBase
    {
        private readonly ICreateBatteryMeasurementService batteryMeasurementService;
        private readonly IGetTemperatureMeasurementsService getTemperatureMeasurementsService;
        private readonly ICreateTemperatureMeasurementService createTemperatureMeasurementService;

        public TemperaturesController(
            IGetTemperatureMeasurementsService getTemperatureMeasurementsService,
            ICreateTemperatureMeasurementService createTemperatureMeasurementService,
            ICreateBatteryMeasurementService batteryMeasurementService)
        {
            this.getTemperatureMeasurementsService = getTemperatureMeasurementsService;
            this.createTemperatureMeasurementService = createTemperatureMeasurementService;
            this.batteryMeasurementService = batteryMeasurementService;
        }
        
        [HttpPost("api/sensors/{sensorId:int}/temperatures")]
        [HttpPost("api/temperatures")]
        public async Task<IActionResult> Temperature(int sensorId, double temperature, double? voltage)
        {
            var result =
                await createTemperatureMeasurementService.CreateAsync(sensorId, temperature, DateTime.Now);
            if (!result.Succeeded)
            {
                return BadRequest(result.ValidationResult.ToString());
            }
            
            // if voltage has been measured 
            if (voltage != null)
            {
                var validationResult = await batteryMeasurementService.CreateAsync(sensorId, voltage.Value, DateTime.Now);

                if (!validationResult.Succeeded)
                {
                    return BadRequest(validationResult.ToString());
                }
            }

            return Ok();
        }
        
        [HttpGet("api/sensors/{sensorId:int}/temperatures")]
        [HttpGet("api/temperatures")]
        public async Task<IActionResult> Temperatures(int? sensorId, DateTime? from, DateTime? to)
        {
            var filter = new MeasurementFilter()
            {
                From = from,
                To = to,
                SensorId = sensorId
            };

            var result = await getTemperatureMeasurementsService.GetFilteredMeasurementsAsync(filter);

            return Ok(result.Select(x => new { x.Temperature, x.MeasurementDateTime, SensorId = x.SensorId }).ToList());
        }

        [HttpGet("api/sensors/{sensorId:int}/temperatures/last")]
        public async Task<IActionResult> LastSensorTemperature(int sensorId)
        {
            var temperatureMeasurement = await getTemperatureMeasurementsService.GetLastMeasurementAsync(sensorId);

            if (temperatureMeasurement == null)
            {
                return BadRequest("Given sensor does not exist or doesn't have temperature measurements.");
            }

            return Ok(temperatureMeasurement.Temperature);
        }
    }
}