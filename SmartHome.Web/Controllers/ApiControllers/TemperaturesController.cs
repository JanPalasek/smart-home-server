using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Web.Controllers.ApiControllers
{
    public class TemperaturesController : ControllerBase
    {
        private readonly ITemperatureMeasurementRepository temperatureMeasurementRepository;
        private readonly IBatteryMeasurementRepository batteryMeasurementRepository;
        private readonly ISensorRepository sensorRepository;

        public TemperaturesController(
            ITemperatureMeasurementRepository temperatureMeasurementRepository,
            IBatteryMeasurementRepository batteryMeasurementRepository,
            ISensorRepository sensorRepository)
        {
            this.temperatureMeasurementRepository = temperatureMeasurementRepository;
            this.batteryMeasurementRepository = batteryMeasurementRepository;
            this.sensorRepository = sensorRepository;
        }
        
        [HttpPost("api/sensors/{sensorId:int}/temperatures")]
        [HttpPost("api/temperatures")]
        public async Task<IActionResult> Temperature(int sensorId, double temperature, double? voltage)
        {
            if (!await sensorRepository.AnyAsync(sensorId))
            {
                return BadRequest($"Sensor with id {sensorId} does not exist.");
            }
            
            await temperatureMeasurementRepository.AddAsync(sensorId, temperature, DateTime.Now);
            
            // if voltage has been measured and the sensor is currently on a battery source => add battery voltage measurement
            if (voltage != null && await sensorRepository.AnyWithBatteryPowerSourceAsync(sensorId))
            {
                await batteryMeasurementRepository.AddAsync(sensorId, voltage.Value, DateTime.Now);
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

            var temperatureMeasurements = await temperatureMeasurementRepository.GetTemperatureMeasurementsAsync(filter);

            return Ok(temperatureMeasurements.Select(x => new { x.Temperature, x.MeasurementDateTime, SensorId = x.SensorId }).ToList());
        }

        [HttpGet("api/sensors/{sensorId:int}/temperatures/last")]
        public async Task<IActionResult> LastSensorTemperature(int sensorId)
        {
            var temperatureMeasurement = await temperatureMeasurementRepository.GetSensorLastTemperatureMeasurementAsync(sensorId);

            if (temperatureMeasurement == null)
            {
                return BadRequest("Given sensor does not exist or doesn't have temperature measurements.");
            }

            return Ok(temperatureMeasurement.Temperature);
        }
    }
}