using SmartHome.DomainCore.Data.Models;

namespace SmartHome.Web.Models.Permission
{
    public class PermissionViewModel
    {
        public PermissionViewModel(PermissionModel model)
        {
            Model = model;
        }
        
        public bool IsCreatePage { get; set; }
        public bool CanEdit { get; set; }
        
        public PermissionModel Model { get; set; }
    }
}