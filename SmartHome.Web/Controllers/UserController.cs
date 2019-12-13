using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartHome.Common.Extensions;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Permission;
using SmartHome.DomainCore.ServiceInterfaces.Role;
using SmartHome.DomainCore.ServiceInterfaces.User;
using SmartHome.Web.Models.Role;
using SmartHome.Web.Models.User;
using SmartHome.Web.Utils;
using Syncfusion.EJ2.Base;

namespace SmartHome.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICreateUserService createUserService;
        private readonly IGetUsersService getUsersService;
        private readonly IUpdateUserService updateUserService;
        private readonly IDeleteUserService deleteUserService;
        private readonly IChangePasswordService changePasswordService;
        private readonly IGetRolesService getRolesService;
        private readonly IGetPermissionsService getPermissionsService;
        private readonly IPermissionVerificationService permissionVerificationService;

        public UserController(ICreateUserService createUserService, IGetUsersService getUsersService,
            IUpdateUserService updateUserService, IChangePasswordService changePasswordService,
            IGetRolesService getRolesService, IDeleteUserService deleteUserService,
            IGetPermissionsService getPermissionsService, IPermissionVerificationService permissionVerificationService)
        {
            this.createUserService = createUserService;
            this.getUsersService = getUsersService;
            this.updateUserService = updateUserService;
            this.changePasswordService = changePasswordService;
            this.getRolesService = getRolesService;
            this.deleteUserService = deleteUserService;
            this.getPermissionsService = getPermissionsService;
            this.permissionVerificationService = permissionVerificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(long id)
        {
            // isn't admin and he isn't trying to change his own password
            if (!await IsAuthorizedToChangeUserSettings(id))
            {
                return Unauthorized();
            }
            
            var model = await getUsersService.GetByIdAsync(id);

            var availableRoles = await getRolesService.GetAllRolesAsync();
            var userRoles = await getRolesService.GetUserRolesAsync(id);

            var permissions = await getPermissionsService.GetAllUserPermissionsWithRolesAsync(id);
            
            var viewModel = new DetailUserViewModel(model, userRoles.Select(x => x.Id).ToList(),
                (List<RoleModel>)availableRoles, (List<PermissionRoleModel>)permissions)
            {
                CanDelete = User.Identity.Name != model.UserName,
                CanEditRoles = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Administration.User.EditAll")
            };

            return View("Detail", viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(UserModel model, List<long> roles)
        {
            // isn't admin and he isn't trying to change his own password
            if (!await IsAuthorizedToChangeUserSettings(model.Id))
            {
                return Unauthorized();
            }
            
            if (ModelState.IsValid)
            {
                // if user attempts to update roles and is not admin => error
                var storedRoles = (await getRolesService.GetUserRolesAsync(model.Id)).Select(x => x.Id).ToList();
                if (!storedRoles.UnorderedEquals(roles) && !await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Administration.User.EditAll"))
                {
                    return Unauthorized();
                }
                
                var result = await updateUserService.AddToOrRemoveFromRolesAsync(model.Id, roles);
                if (result.Succeeded)
                {
                    return RedirectToAction("Detail", new {id = model.Id});
                }
                
                ModelState.AddValidationErrors(result);
            }
            
            var availableRoles = await getRolesService.GetAllRolesAsync();
            var permissions = await getPermissionsService.GetAllUserPermissionsWithRolesAsync(model.Id);
            
            return View("Detail", new DetailUserViewModel(model, roles,
                (List<RoleModel>)availableRoles, (List<PermissionRoleModel>)permissions)
            {
                CanDelete = User.Identity.Name != model.UserName,
                CanEditRoles = await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Administration.User.EditAll")
            });
        }

        [HttpGet]
        [Authorize(Policy = "Administration.User.ViewAll")]
        public async Task<IActionResult> List()
        {
            var users = await getUsersService.GetAllUsersAsync();

            return View("List", new UserListViewModel(users));
        }

        [HttpGet]
        [Authorize(Policy = "Administration.User.EditAll")]
        public IActionResult Create()
        {
            var viewModel = new CreateUserViewModel(new CreateUserModel());

            return View("Create", viewModel);
        }
        
        [HttpPost]
        [Authorize(Policy = "Administration.User.EditAll")]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await createUserService.CreateUserAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Detail", new { id = (await getUsersService.GetByEmailAsync(model.Email!))!.Id });
                }
                
                ModelState.AddValidationErrors(result);
            }

            return View("Create", new CreateUserViewModel(model));
        }

        [HttpGet]
        [Authorize(Policy = "Administration.User.EditAll")]
        public async Task<IActionResult> Delete(long id)
        {
            // cannot delete your own account
            if ((await getUsersService.GetByNameAsync(User.Identity.Name!))?.Id == id)
            {
                return Unauthorized();
            }
            
            var result = await deleteUserService.DeleteUserAsync(id);

            if (result.Succeeded)
            {
                return RedirectToAction("List");
            }
            
            throw new ArgumentException("User cannot be deleted.");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(long id)
        {
            // isn't admin and he isn't trying to change his own password
            if (!await IsAuthorizedToChangeUserSettings(id))
            {
                return Unauthorized();
            }
            
            var model = await getUsersService.GetByIdAsync(id);
            
            var vm = new ChangePasswordViewModel(new ChangePasswordModel() { Id = model.Id });
            return View("ChangePassword", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            // isn't admin and he isn't trying to change his own password
            if (!await IsAuthorizedToChangeUserSettings(model.Id))
            {
                return Unauthorized();
            }
            
            if (ModelState.IsValid)
            {
                var result = await changePasswordService.ChangePasswordAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Detail", new {id = model.Id});
                }

                ModelState.AddValidationErrors(result);
            }

            return View("ChangePassword", new ChangePasswordViewModel(model));
        }

        [HttpGet]
        public async Task<IActionResult> PermissionsList(long userId)
        {
            if (!await IsAuthorizedToViewUserSettings(userId))
            {
                return Unauthorized();
            }
            
            var vm = new UserPermissionsViewModel(userId);
            vm.PermissionOptions = await getPermissionsService.GetAllPermissionsAsync();
            return View("PermissionsList", vm);
        }
        
        [Authorize(Policy = "Administration.User.ViewAll")]
        [Authorize(Policy = "Administration.Permission.View")]
        public async Task<IActionResult> PermissionsDataSource(long userId, [FromBody]DataManagerRequest dm)
        {
            if (!await IsAuthorizedToViewUserSettings(userId))
            {
                return Unauthorized();
            }
            
            var result =
                await getPermissionsService.GetUserOnlyPermissionsAsync(userId);
            return dm.RequiresCounts ? Json(new { result = result, count = result.Count }) : Json(result);
        }
        
        [Authorize(Policy = "Administration.User.EditAll")]
        public async Task<IActionResult> PermissionsUpdate(long userId, [FromBody]CRUDModel batchModel)
        {
            if (!await IsAuthorizedToChangeUserSettings(userId))
            {
                return Unauthorized();
            }
            
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
                .Select(x => x.Name!)
                .ToList();

            var result = await updateUserService.UpdatePermissionsAsync(userId, removed, updated, added);
            if (!result.Succeeded)
            {
                return BadRequest(result.ToString());
            }
            
            
            var permissions =
                await getPermissionsService.GetUserOnlyPermissionsAsync(userId);
            
            return Json(permissions);
        }
            
        private async Task<bool> IsAuthorizedToChangeUserSettings(long userId)
        {
            if (User?.Identity?.Name == null)
            {
                return false;
            }
            
            // can edit all
            if (await permissionVerificationService.HasPermissionAsync(User.Identity.Name, "Administration.User.EditAll"))
            {
                return true;
            }

            // is user's own profile
            if ((await getUsersService.GetByNameAsync(User.Identity.Name))?.Id == userId)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> IsAuthorizedToViewUserSettings(long userId)
        {
            // can view all or is user's own profile
            if (await permissionVerificationService.HasPermissionAsync(User.Identity.Name!, "Administration.User.ViewAll") || 
                (await getUsersService.GetByNameAsync(User.Identity.Name!))?.Id == userId)
            {
                return true;
            }

            return false;
        }
    }
}