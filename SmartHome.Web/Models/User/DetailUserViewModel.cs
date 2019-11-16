using System.Collections.Generic;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.User
{
    public class DetailUserViewModel
    {
        public bool CanDelete { get; set; }
        public bool CanEditRoles { get; set; }
        public List<RoleModel> AvailableRoles { get; set; }
        
        public UserModel Model { get; set; }
        public List<long> Roles { get; set; }
        
        public DetailUserViewModel(UserModel model, List<long> roles, List<RoleModel> availableRoles)
        {
            Model = model;
            Roles = roles;
            AvailableRoles = availableRoles;
        }
    }
}