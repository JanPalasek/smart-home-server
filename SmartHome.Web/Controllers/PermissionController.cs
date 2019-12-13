using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Permission;
using SmartHome.Web.Models.BatteryPowerSourceType;
using SmartHome.Web.Models.Permission;
using SmartHome.Web.Utils;

namespace SmartHome.Web.Controllers
{
    [Authorize(Policy = "Administration.Permission.View")]
    public class PermissionController : Controller
    {
        private readonly ICreatePermissionService createPermissionService;
        private readonly IUpdatePermissionService updatePermissionService;
        private readonly IGetPermissionsService getPermissionsService;
        private readonly IDeletePermissionService deletePermissionService;
        private readonly IPermissionVerificationService permissionVerificationService;

        public PermissionController(ICreatePermissionService createPermissionService,
            IUpdatePermissionService updatePermissionService,
            IGetPermissionsService getPermissionsService,
            IDeletePermissionService deletePermissionService, IPermissionVerificationService permissionVerificationService)
        {
            this.createPermissionService = createPermissionService;
            this.updatePermissionService = updatePermissionService;
            this.getPermissionsService = getPermissionsService;
            this.deletePermissionService = deletePermissionService;
            this.permissionVerificationService = permissionVerificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await getPermissionsService.GetByIdAsync(id);

            var vm = new PermissionViewModel(model)
            {
                CanEdit = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Administration.Permission.Edit"),
                IsCreatePage = false
            };

            return View("Detail", vm);
        }

        [HttpPost]
        [Authorize(Policy = "Administration.Permission.Edit")]
        public async Task<IActionResult> Detail(PermissionModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await updatePermissionService.UpdateAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Detail", new {id = model.Id});
                }
                
                ModelState.AddValidationErrors(result);
            }

            return View("Detail", new PermissionViewModel(model)
            {
                CanEdit = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Administration.Permission.Edit"),
                IsCreatePage = false
            });
        }

        [HttpGet]
        [Authorize(Policy = "Administration.Permission.Edit")]
        public IActionResult Create()
        {
            var vm = new PermissionViewModel(new PermissionModel())
            {
                CanEdit = User.IsInRole("Admin"),
                IsCreatePage = true
            };

            return View("Detail", vm);
        }
        
        [HttpPost]
        [Authorize(Policy = "Administration.Permission.Edit")]
        public async Task<IActionResult> Create(PermissionModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await createPermissionService.CreateAsync(model);

                if (result.Succeeded)
                {
                    long id = result.Value;
                    return RedirectToAction("Detail", new {id});
                }
                
                ModelState.AddValidationErrors(result.ValidationResult);
            }

            return View("Detail", new PermissionViewModel(model)
            {
                CanEdit = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Administration.Permission.Edit"),
                IsCreatePage = true
            });
        }

        [HttpGet]
        [Authorize(Policy = "Administration.Permission.Edit")]
        public async Task<IActionResult> Delete(long id)
        {
            await deletePermissionService.DeleteAsync(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var permissions = await getPermissionsService.GetAllPermissionsAsync();
            return View("List", new PermissionListViewModel(permissions)
            {
                CanCreate = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Administration.Permission.Edit")
            });
        }
    }
}