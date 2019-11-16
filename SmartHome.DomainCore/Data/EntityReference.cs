namespace SmartHome.DomainCore.Data
{
    public class EntityReference
    {
        public long Id { get; }
        public string? Name { get;  }
        
        public EntityReference(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}