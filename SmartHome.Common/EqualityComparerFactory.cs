namespace SmartHome.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Utility class for creating <see cref="IEqualityComparer{TType}"/> instances 
    /// from Lambda expressions.
    /// </summary>
    /// <remarks>
    /// From https://stackoverflow.com/questions/3189861/pass-a-lambda-expression-in-place-of-icomparer-or-iequalitycomparer-or-any-singl.
    /// </remarks>
    public static class EqualityComparerFactory
    {
        /// <summary>Creates the specified <see cref="IEqualityComparer{TType}" />.</summary>
        /// <typeparam name="TType">The type to compare.</typeparam>
        /// <param name="getHashCode">The get hash code delegate.</param>
        /// <param name="equals">The equals delegate.</param>
        /// <returns>An instance of <see cref="IEqualityComparer{TType}" />.</returns>
        public static IEqualityComparer<TType> Create<TType>(
            Func<TType, int> getHashCode,
            Func<TType, TType, bool> equals)
        {
            return new Comparer<TType>(getHashCode, equals);
        }

        private class Comparer<TType> : IEqualityComparer<TType>
        {
            private readonly Func<TType, int> getHashCode;
            private readonly Func<TType, TType, bool> @equals;

            public Comparer(Func<TType, int> getHashCode, Func<TType, TType, bool> equals)
            {
                this.getHashCode = getHashCode;
                this.@equals = equals;
            }

            public bool Equals(TType x, TType y) => @equals(x, y);

            public int GetHashCode(TType obj) => getHashCode(obj);
        }
    }
}