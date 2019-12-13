using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Permission;
using SmartHome.DomainCore.ServiceInterfaces.Place;
using SmartHome.DomainCore.ServiceInterfaces.User;
using SmartHome.Web.Models.Place;

namespace SmartHome.Web.Controllers
{
    [Authorize(Policy = "Enumeration.Place.View")]
    public class PlaceController : Controller
    {
        private readonly IGetPlacesService getPlacesService;
        private readonly ICreatePlaceService createPlaceService;
        private readonly IUpdatePlaceService updatePlaceService;
        private readonly IDeletePlaceService deletePlaceService;
        private readonly IPermissionVerificationService permissionVerificationService;

        public PlaceController(IGetPlacesService getPlacesService, ICreatePlaceService createPlaceService,
            IUpdatePlaceService updatePlaceService, IDeletePlaceService deletePlaceService, IPermissionVerificationService permissionVerificationService)
        {
            this.getPlacesService = getPlacesService;
            this.createPlaceService = createPlaceService;
            this.updatePlaceService = updatePlaceService;
            this.deletePlaceService = deletePlaceService;
            this.permissionVerificationService = permissionVerificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            PlaceModel placeModel = await getPlacesService.GetPlaceAsync(id);
            
            return View("Detail", new PlaceViewModel(placeModel)
            {
                CanEdit = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Enumeration.Place.Edit")
            });
        }

        [HttpPost]
        [Authorize(Policy = "Enumeration.Place.Edit")]
        public async Task<IActionResult> Detail(PlaceModel model)
        {
            if (ModelState.IsValid)
            {
                await updatePlaceService.UpdateAsync(model);

                return RedirectToAction("Detail", new {model.Id});
            }

            return View("Detail", new PlaceViewModel(model)
            {
                CanEdit = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Enumeration.Place.Edit")
            });
        }

        [HttpGet]
        [Authorize(Policy = "Enumeration.Place.Edit")]
        public async Task<IActionResult> Delete(long id)
        {
            await deletePlaceService.DeletePlaceAsync(id);

            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize(Policy = "Enumeration.Place.Edit")]
        public async Task<IActionResult> Create()
        {
            return View("Detail", new PlaceViewModel(new PlaceModel())
            {
                CanEdit = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Enumeration.Place.Edit"),
                IsCreatePage = true
            });
        }

        [HttpPost]
        [Authorize(Policy = "Enumeration.Place.Edit")]
        public async Task<IActionResult> Create(PlaceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Detail", new PlaceViewModel(model)
                {
                    CanEdit = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Enumeration.Place.Edit"),
                    IsCreatePage = true
                });
            }

            long id = await createPlaceService.CreateAsync(model);
            
            return RedirectToAction("Detail", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var items = await getPlacesService.GetAllPlacesAsync();
            
            var viewModel = new PlaceListViewModel(items)
            {
                CanCreate = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Enumeration.Place.Edit")
            };
            
            return View("List", viewModel);
        }
    }
}