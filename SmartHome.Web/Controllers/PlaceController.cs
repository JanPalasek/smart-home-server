using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;
using SmartHome.Web.Models.Place;

namespace SmartHome.Web.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IPlaceRepository repository;

        public PlaceController(IPlaceRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            PlaceModel placeModel;
            if (id == null)
            {
                placeModel = new PlaceModel();
            }
            else
            {
                placeModel = await repository.SingleAsync(id.Value);
            }

            return View("Detail", new PlaceViewModel {Model = placeModel});
        }

        [HttpPost]
        public async Task<IActionResult> Detail(PlaceModel model)
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