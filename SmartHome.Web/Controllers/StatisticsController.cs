using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Common;
using SmartHome.Common.DateTimeProviders;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.ServiceInterfaces.TemperatureMeasurement;
using SmartHome.Web.Models.Statistics;
using Syncfusion.EJ2.Base;
using Syncfusion.EJ2.Charts;

namespace SmartHome.Web.Controllers
{
    [Authorize(Policy = "Statistic.View")]
    public class StatisticsController : Controller
    {
        private readonly IGetTemperatureMeasurementsService getTemperatureMeasurementsService;
        private readonly IDateTimeProvider dateTimeProvider;

        public StatisticsController(IGetTemperatureMeasurementsService getTemperatureMeasurementsService, IDateTimeProvider dateTimeProvider)
        {
            this.getTemperatureMeasurementsService = getTemperatureMeasurementsService;
            this.dateTimeProvider = dateTimeProvider;
        }
        
        [HttpGet("Statistics")]
        public IActionResult Index()
        {
            var vm = new StatisticsViewModel(
                new StatisticsFilter()
                {
                    DateFrom = dateTimeProvider.Today.AddYears(-1)
                });
            return View("Statistics", vm);
        }
        
        [HttpGet]
        public async Task<IActionResult> StatisticsDataSource([Bind(Prefix = "")]StatisticsFilter statisticsFilter)
        {
            var resultMeasurementStatisticsModel = await getTemperatureMeasurementsService
                .GetFilteredMeasurementAsync(statisticsFilter);

            return Json(resultMeasurementStatisticsModel);
        }
    }
}