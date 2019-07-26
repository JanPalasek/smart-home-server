namespace SmartHome.Database.Utils
{
    using System.Collections.Generic;
    using Entities;
    public class EntityEqualityComparer : IEqualityComparer<Entity>
    {
        public bool Equals(Entity x, Entity y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Entity obj)
        {
            unchecked
            {
                return (int)obj.Id;
            }
        }
    }
}