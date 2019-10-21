namespace SmartHome.Shared
{
    public class EntityReference
    {
        public int Id { get; }
        public string? Name { get;  }
        
        public EntityReference(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}