using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Admin;
using SmartHome.Web.Models.Admin;
using SmartHome.Web.Utils;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICreateUserService createUserService;
        private readonly IGetUsersService getUsersService;
        private readonly IChangePasswordService changePasswordService;
        private readonly ICreateRoleService createRoleService;
        private readonly IUpdateRoleService updateRoleService;
        private readonly IGetRolesService getRolesService;

        public AdminController(ICreateUserService createUserService, IGetUsersService getUsersService,
            IChangePasswordService changePasswordService, ICreateRoleService createRoleService,
            IUpdateRoleService updateRoleService, IGetRolesService getRolesService)
        {
            this.createUserService = createUserService;
            this.getUsersService = getUsersService;
            this.changePasswordService = changePasswordService;
            this.createRoleService = createRoleService;
            this.updateRoleService = updateRoleService;
            this.getRolesService = getRolesService;
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(long id)
        {
            var model = await getUsersService.GetByIdAsync(id);

            var availableRoles = await getRolesService.GetAllRolesAsync();
            var userRoles = await getRolesService.GetUserRolesAsync(id);
            
            var viewModel = new DetailUserViewModel(model, (List<RoleModel>)userRoles,
                (List<RoleModel>)availableRoles);

            return View("UserDetail", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = await getUsersService.GetAllUsersAsync();

            return View("UserList", new UserListViewModel(users));
        }

        [HttpGet]
        public IActionResult UserCreate()
        {
            var viewModel = new CreateUserViewModel(new CreateUserModel());

            return View("UserCreate", viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> UserCreate(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await createUserService.CreateUserAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new { id = (await getUsersService.GetByEmailAsync(model.Email!))!.Id });
                }
                
                // print errors
                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError(identityError.Code, identityError.Description);
                }
            }

            return View("UserCreate", new CreateUserViewModel(model));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(long id)
        {
            var model = await getUsersService.GetByIdAsync(id);
            
            var vm = new ChangePasswordViewModel(new ChangePasswordModel() { Id = model.Id });
            return View("ChangePassword", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await changePasswordService.ChangePasswordAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new {id = model.Id});
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return View("ChangePassword", new ChangePasswordViewModel(model));
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
                    long roleId = (await getRolesService.GetRoleByNameAsync(model.Name)).Id;
                    return RedirectToAction("RoleDetail", new {id = roleId});
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
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
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
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