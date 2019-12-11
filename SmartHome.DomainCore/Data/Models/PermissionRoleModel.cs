namespace SmartHome.DomainCore.Data.Models
{
    public class PermissionRoleModel
    {
        public long PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public string? RoleName { get; set; }

        public bool Inherited => !string.IsNullOrEmpty(RoleName);
    }
}