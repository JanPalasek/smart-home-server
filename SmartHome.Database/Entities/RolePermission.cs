namespace SmartHome.Database.Entities
{
    public class RolePermission : Entity
    {
        public long PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}