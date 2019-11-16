using System.Collections.Generic;
using System.Linq;

namespace SmartHome.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/> interface.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Split into <see cref="batchSize"/> batches.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="batchSize">Size of batch to split into.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>( 
            this IEnumerable<T> enumerable, int batchSize)
        {
            using var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext()) 
                yield return YieldBatchElements(enumerator, batchSize - 1);
        } 

        private static IEnumerable<T> YieldBatchElements<T>( 
            IEnumerator<T> enumerable, int batchSize) 
        { 
            yield return enumerable.Current; 
            for (int i = 0; i < batchSize && enumerable.MoveNext(); i++) 
                yield return enumerable.Current; 
        }

        /// <summary>
        /// Performs unordered equality comparison between <see cref="enumerable"/> and <see cref="collection"/>.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="collection"></param>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        public static bool UnorderedEquals<TType>(this IEnumerable<TType> enumerable, ICollection<TType> collection)
        {
            foreach (var item in enumerable)
            {
                if (!collection.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}