using System.Collections.Generic;

namespace SmartHome.DomainCore.Data
{
    public class CountedResult<TType>
    {
        public CountedResult(int count, IList<TType> items)
        {
            Count = count;
            Items = items;
        }

        public int Count { get; }
        public IList<TType> Items { get; }
    }
}