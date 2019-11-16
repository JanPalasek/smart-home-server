using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Role;
using SmartHome.Web.Models.Role;
using SmartHome.Web.Models.User;
using SmartHome.Web.Utils;

namespace SmartHome.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly ICreateRoleService createRoleService;
        private readonly IUpdateRoleService updateRoleService;
        private readonly IGetRolesService getRolesService;

        public RoleController(
            ICreateRoleService createRoleService,
            IUpdateRoleService updateRoleService,
            IGetRolesService getRolesService)
        {
            this.createRoleService = createRoleService;
            this.updateRoleService = updateRoleService;
            this.getRolesService = getRolesService;
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View("RoleCreate", new CreateRoleViewModel(new CreateRoleModel()));
        }
        
        [HttpPost]
        public async Task<IActionResult> RoleCreate(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await createRoleService.CreateRoleAsync(model);
                if (result.Succeeded)
                {
                    long roleId = (await getRolesService.GetRoleByNameAsync(model.Name!)).Id;
                    return RedirectToAction("RoleDetail", new {id = roleId});
                }
                
                ModelState.AddValidationErrors(result);
            }

            return View("RoleCreate", new CreateRoleViewModel(model));
        }
        
        [HttpGet]
        public async Task<IActionResult> RoleDetail(int id)
        {
            var role = await getRolesService.GetRoleByIdAsync(id);

            return View("RoleDetail", new DetailRoleViewModel(role));
        }
        
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await updateRoleService.UpdateRoleAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleDetail", new {id = model.Id});
                }
                
                ModelState.AddValidationErrors(result);
            }

            return View("RoleDetail", new DetailRoleViewModel(model));
        }

        [HttpGet]
        public async Task<IActionResult> RoleList()
        {
            var roles = await getRolesService.GetAllRolesAsync();

            return View("RoleList", new RoleListViewModel(roles));
        }
    }
}