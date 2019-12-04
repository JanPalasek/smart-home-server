namespace SmartHome.Database.Entities
{
    public class UserPermission : Entity
    {
        public long PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}