using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Place;
using SmartHome.DomainCore.ServiceInterfaces.User;
using SmartHome.Web.Models.Place;

namespace SmartHome.Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class PlaceController : Controller
    {
        private readonly IGetPlacesService getPlacesService;
        private readonly ICreatePlaceService createPlaceService;
        private readonly IUpdatePlaceService updatePlaceService;
        private readonly IDeletePlaceService deletePlaceService;

        public PlaceController(IGetPlacesService getPlacesService, ICreatePlaceService createPlaceService,
            IUpdatePlaceService updatePlaceService, IDeletePlaceService deletePlaceService)
        {
            this.getPlacesService = getPlacesService;
            this.createPlaceService = createPlaceService;
            this.updatePlaceService = updatePlaceService;
            this.deletePlaceService = deletePlaceService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            PlaceModel placeModel = await getPlacesService.GetPlaceAsync(id);
            
            return View("Detail", new PlaceViewModel(placeModel) { CanEdit = User.IsInRole("Admin") });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(PlaceModel model)
        {
            if (ModelState.IsValid)
            {
                await updatePlaceService.UpdateAsync(model);

                return RedirectToAction("Detail", new {model.Id});
            }

            return View("Detail", new PlaceViewModel(model) { CanEdit = User.IsInRole("Admin") });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            await deletePlaceService.DeletePlaceAsync(id);

            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View("Detail", new PlaceViewModel(new PlaceModel()) {CanEdit = User.IsInRole("Admin")});
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PlaceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new PlaceViewModel(model) { CanEdit = User.IsInRole("Admin") });
            }

            long id = await createPlaceService.CreateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var items = await getPlacesService.GetAllPlacesAsync();
            
            var viewModel = new PlaceListViewModel(items) { CanCreate = User.IsInRole("Admin") };
            
            return View("List", viewModel);
        }
    }
}