using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.Vehicle.Service.Common.Helper
{
    public static class DistinctExtensions
    {
        public static IEnumerable<T> Distinct<T, TV>(
            this IEnumerable<T> source,
            Func<T, TV> keySelector)
        {
            return source.Distinct<T>((IEqualityComparer<T>)new DistinctEqualit<T, TV>(keySelector));
        }
    }

    public class DistinctEqualit<T, TV> : IEqualityComparer<T>
    {
        private readonly Func<T, TV> _keySelector;

        public DistinctEqualit(Func<T, TV> keySelector)
        {
            this._keySelector = keySelector;
        }

        public bool Equals(T x, T y)
        {
            return EqualityComparer<TV>.Default.Equals(this._keySelector(x), this._keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return EqualityComparer<TV>.Default.GetHashCode(this._keySelector(obj));
        }
    }
}
