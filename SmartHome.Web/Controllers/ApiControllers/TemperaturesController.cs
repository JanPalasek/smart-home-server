using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Common;
using SmartHome.Common.DateTimeProviders;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.BatteryMeasurement;
using SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement;

namespace SmartHome.Web.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TemperaturesController : ControllerBase
    {
        private readonly ICreateBatteryMeasurementService batteryMeasurementService;
        private readonly IGetTemperatureMeasurementsService getTemperatureMeasurementsService;
        private readonly ICreateTemperatureMeasurementService createTemperatureMeasurementService;
        private readonly IDateTimeProvider dateTimeProvider;

        public TemperaturesController(
            IGetTemperatureMeasurementsService getTemperatureMeasurementsService,
            ICreateTemperatureMeasurementService createTemperatureMeasurementService,
            ICreateBatteryMeasurementService batteryMeasurementService, IDateTimeProvider dateTimeProvider)
        {
            this.getTemperatureMeasurementsService = getTemperatureMeasurementsService;
            this.createTemperatureMeasurementService = createTemperatureMeasurementService;
            this.batteryMeasurementService = batteryMeasurementService;
            this.dateTimeProvider = dateTimeProvider;
        }
        
        [HttpPost("api/sensors/{sensorId:int}/temperatures")]
        [HttpPost("api/temperatures")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Policy = "Measurement.Temperature.Edit")]
        public async Task<IActionResult> Temperature(int sensorId, double temperature, double? voltage)
        {
            var result =
                await createTemperatureMeasurementService.CreateAsync(sensorId, temperature, dateTimeProvider.Now);
            if (!result.Succeeded)
            {
                return BadRequest(result.ValidationResult.ToString());
            }
            
            // if voltage has been measured 
            if (voltage != null)
            {
                var validationResult = await batteryMeasurementService.CreateAsync(sensorId, voltage.Value, dateTimeProvider.Now);

                if (!validationResult.Succeeded)
                {
                    return BadRequest(validationResult.ToString());
                }
            }

            return Ok();
        }
        
        [HttpGet("api/sensors/{sensorId:int}/temperatures")]
        [HttpGet("api/temperatures")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Policy = "Measurement.Temperature.View")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Policy = "Measurement.Temperature.View")]
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