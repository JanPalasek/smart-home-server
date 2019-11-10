using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Place;
using SmartHome.Web.Models.Place;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class PlaceController : Controller
    {
        private readonly IGetPlacesService getPlacesService;
        private readonly ICreatePlaceService createPlaceService;
        private readonly IUpdatePlaceService updatePlaceService;

        public PlaceController(IGetPlacesService getPlacesService, ICreatePlaceService createPlaceService, IUpdatePlaceService updatePlaceService)
        {
            this.getPlacesService = getPlacesService;
            this.createPlaceService = createPlaceService;
            this.updatePlaceService = updatePlaceService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            PlaceModel placeModel = await getPlacesService.GetPlaceAsync(id);
            
            return View("Detail", new PlaceViewModel(placeModel));
        }

        [HttpPost]
        public async Task<IActionResult> Detail(PlaceModel model)
        {
            if (ModelState.IsValid)
            {
                await updatePlaceService.UpdateAsync(model);

                return RedirectToAction("Detail", new {model.Id});
            }

            return View("Detail", new PlaceViewModel(model));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Detail", new PlaceViewModel(new PlaceModel()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlaceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new PlaceViewModel(model));
            }

            long id = await createPlaceService.CreateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var items = await getPlacesService.GetAllPlacesAsync();
            
            var viewModel = new PlaceListViewModel(items);
            
            return View("List", viewModel);
        }
    }
}