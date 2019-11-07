using System.Collections.Generic;
using SmartHome.DomainCore;

namespace SmartHome.Infrastructure.Utils
{
    public class EntityEqualityComparer : IEqualityComparer<IId<long>>
    {
        public bool Equals(IId<long> x, IId<long> y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(IId<long> obj)
        {
            unchecked
            {
                return (int)obj.Id;
            }
        }
    }
}