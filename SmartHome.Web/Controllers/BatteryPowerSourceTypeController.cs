using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.BatteryPowerSourceType;
using SmartHome.Web.Models.BatteryPowerSourceType;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class BatteryPowerSourceTypeController : Controller
    {
        private readonly IUpdateBatteryPowerSourceTypeService updateBatteryPowerSourceTypeService;
        private readonly ICreateBatteryPowerSourceTypeService createBatteryPowerSourceTypeService;
        private readonly IGetBatteryPowerSourceTypesService getBatteryPowerSourceTypesService;
        private readonly IDeleteBatteryPowerSourceTypeService deleteBatteryPowerSourceTypeService;
        public BatteryPowerSourceTypeController(IUpdateBatteryPowerSourceTypeService updateBatteryPowerSourceTypeService,
            ICreateBatteryPowerSourceTypeService createBatteryPowerSourceTypeService,
            IGetBatteryPowerSourceTypesService getBatteryPowerSourceTypesService,
            IDeleteBatteryPowerSourceTypeService deleteBatteryPowerSourceTypeService)
        {
            this.updateBatteryPowerSourceTypeService = updateBatteryPowerSourceTypeService;
            this.createBatteryPowerSourceTypeService = createBatteryPowerSourceTypeService;
            this.getBatteryPowerSourceTypesService = getBatteryPowerSourceTypesService;
            this.deleteBatteryPowerSourceTypeService = deleteBatteryPowerSourceTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            BatteryPowerSourceTypeModel sensorModel = await getBatteryPowerSourceTypesService.GetByIdAsync(id);

            var vm = new BatteryPowerSourceTypeViewModel(sensorModel)
            {
                CanEdit = User.IsInRole("Admin"),
                IsCreatePage = false
            };

            return View("Detail", vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(BatteryPowerSourceTypeModel model)
        {
            if (ModelState.IsValid)
            {
                await updateBatteryPowerSourceTypeService.UpdateAsync(model);

                return RedirectToAction("Detail", new {id = model.Id});
            }

            return View("Detail", new BatteryPowerSourceTypeViewModel(model)
            {
                CanEdit = User.IsInRole("Admin"),
                IsCreatePage = false
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new BatteryPowerSourceTypeViewModel(new BatteryPowerSourceTypeModel())
            {
                CanEdit = User.IsInRole("Admin"),
                IsCreatePage = true
            };

            return View("Detail", vm);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BatteryPowerSourceTypeModel model)
        {
            if (ModelState.IsValid)
            {
                long id = await createBatteryPowerSourceTypeService.CreateAsync(model);

                return RedirectToAction("Detail", new {id});
            }

            return View("Detail", new BatteryPowerSourceTypeViewModel(model) {CanEdit = User.IsInRole("Admin"), IsCreatePage = true});
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            await deleteBatteryPowerSourceTypeService.DeleteAsync(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var sensorTypes = await getBatteryPowerSourceTypesService.GetAllPowerSourceTypesAsync();
            return View("List", new BatteryPowerSourceTypeListViewModel(sensorTypes) { CanCreate = User.IsInRole("Admin") });
        }
    }
}