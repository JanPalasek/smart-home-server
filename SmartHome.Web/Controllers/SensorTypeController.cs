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
        public async Task<IActionResult> Detail(int? id)
        {
            SensorTypeModel sensorModel;
            if (id != null)
            {
                sensorModel = await repository.SingleAsync(id.Value);
            }
            else
            {
                sensorModel = new SensorTypeModel();
            }

            var vm = new SensorTypeViewModel {Model = sensorModel};

            return View("Detail", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Detail(SensorTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new SensorTypeViewModel {Model = model});
            }

            long id = await repository.AddOrUpdateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }
    }
}