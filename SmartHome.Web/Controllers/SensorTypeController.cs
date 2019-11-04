using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;
using SmartHome.Web.Models.Sensor;
using SmartHome.Web.Models.SensorType;
using SmartHome.Web.Utils;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class SensorTypeController : Controller
    {
        private readonly ISensorTypeRepository repository;

        public SensorTypeController(
            ISensorTypeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            SensorTypeModel sensorModel = await repository.SingleAsync(id);

            var vm = new SensorTypeViewModel(sensorModel);

            return View("Detail", vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new SensorTypeViewModel(new SensorTypeModel());

            return View("Detail", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(SensorTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new SensorTypeViewModel(model));
            }

            long id = await repository.AddOrUpdateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }
    }
}