using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;
using SmartHome.Web.Models.Place;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class PlaceController : Controller
    {
        private readonly IPlaceRepository repository;

        public PlaceController(IPlaceRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            PlaceModel placeModel = await repository.SingleAsync(id);
            
            return View("Detail", new PlaceViewModel {Model = placeModel});
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Detail", new PlaceViewModel() {Model = new PlaceModel()});
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlaceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new PlaceViewModel() {Model = model});
            }

            long id = await repository.AddOrUpdateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }
    }
}