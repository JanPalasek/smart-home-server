using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Permission;
using SmartHome.DomainCore.ServiceInterfaces.Role;
using SmartHome.Web.Models.Role;
using SmartHome.Web.Models.User;
using SmartHome.Web.Utils;
using Syncfusion.EJ2.Base;

namespace SmartHome.Web.Controllers
{
    [Authorize(Policy = "Administration.Role.View")]
    public class RoleController : Controller
    {
        private readonly ICreateRoleService createRoleService;
        private readonly IUpdateRoleService updateRoleService;
        private readonly IGetRolesService getRolesService;
        private readonly IGetPermissionsService getPermissionsService;

        public RoleController(
            ICreateRoleService createRoleService,
            IUpdateRoleService updateRoleService,
            IGetRolesService getRolesService,
            IGetPermissionsService getPermissionsService)
        {
            this.createRoleService = createRoleService;
            this.updateRoleService = updateRoleService;
            this.getRolesService = getRolesService;
            this.getPermissionsService = getPermissionsService;
        }

        [HttpGet]
        [Authorize(Policy = "Administration.Role.Edit")]
        public IActionResult Create()
        {
            return View("Create", new CreateRoleViewModel(new CreateRoleModel()));
        }
        
        [HttpPost]
        [Authorize(Policy = "Administration.Role.Edit")]
        public async Task<IActionResult> Create(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await createRoleService.CreateRoleAsync(model);
                if (result.Succeeded)
                {
                    long roleId = (await getRolesService.GetRoleByNameAsync(model.Name!)).Id;
                    return RedirectToAction("Detail", new {id = roleId});
                }
                
                ModelState.AddValidationErrors(result);
            }

            return View("Create", new CreateRoleViewModel(model));
        }
        
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var role = await getRolesService.GetRoleByIdAsync(id);
            var permissions = await getPermissionsService.GetRolePermissionsAsync(id);

            return View("Detail", new DetailRoleViewModel(role, permissions));
        }
        
        [HttpPost]
        [Authorize(Policy = "Administration.Role.Edit")]
        public async Task<IActionResult> Update(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await updateRoleService.UpdateRoleAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Detail", new {id = model.Id});
                }
                
                ModelState.AddValidationErrors(result);
            }

            var permissions = await getPermissionsService.GetRolePermissionsAsync(model.Id);
            return View("Detail", new DetailRoleViewModel(model, permissions));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var roles = await getRolesService.GetAllRolesAsync();

            return View("List", new RoleListViewModel(roles));
        }
        
        [HttpGet]
        [Authorize(Policy = "Administration.Permission.View")]
        public async Task<IActionResult> PermissionsList(long roleId)
        {
            var vm = new RolePermissionsViewModel(roleId);
            vm.PermissionOptions = await getPermissionsService.GetAllPermissionsAsync();
            return View("PermissionsList", vm);
        }
        
        [Authorize(Policy = "Administration.Permission.View")]
        public async Task<IActionResult> PermissionsDataSource(long roleId, [FromBody]DataManagerRequest dm)
        {
            var result =
                await getPermissionsService.GetRolePermissionsAsync(roleId);
            return dm.RequiresCounts ? Json(new { result = result, count = result.Count }) : Json(result);
        }
        
        [Authorize(Policy = "Administration.Permission.View")]
        [Authorize(Policy = "Administration.Role.Edit")]
        public async Task<IActionResult> PermissionsUpdate(long roleId, [FromBody]CRUDModel batchModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var removed =  batchModel.Deleted.Count > 0 ? batchModel.Deleted.Select(x => JsonConvert.DeserializeObject<PermissionModel>(x.ToString()))
                .Select(x => x.Id) : Enumerable.Empty<long>();
            var updated = batchModel.Changed.Count > 0 ? batchModel.Changed.Select(x => JsonConvert.DeserializeObject<PermissionModel>(x.ToString()))
                .Where(x => x.Name != null)
                .Select(x => (x.Id, x.Name!)) : Enumerable.Empty<ValueTuple<long, string>>();
            var added = batchModel.Added.Select(x => JsonConvert.DeserializeObject<PermissionModel>(x.ToString()))
                .Where(x => x.Name != null)
                .Select(x => x.Name!).ToList();

            var result = await updateRoleService.UpdatePermissionsAsync(roleId, removed, updated, added);
            if (!result.Succeeded)
            {
                return BadRequest(result.ToString());
            }
            
            
            var permissions =
                await getPermissionsService.GetRolePermissionsAsync(roleId);
            
            return Json(permissions);
        }
    }
}