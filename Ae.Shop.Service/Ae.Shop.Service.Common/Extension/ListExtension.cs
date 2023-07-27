using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ae.Shop.Service.Common.Constant;

namespace Ae.Shop.Service.Common.Extension
{
    public static class ListExtension
    {
        public static List<T> MoveToFront<T>(this ICollection<T> list, Predicate<T> matchPredicate)
        {
            var arr = list.ToArray();
            if (arr.Length == CommonConstant.Zero)
            {
                return arr.ToList();
            }

            var idx = Array.FindIndex(arr, matchPredicate);
            if (idx == CommonConstant.OneNeg || idx == CommonConstant.Zero)
            {
                return arr.ToList();
            }

            var tmp = arr[idx];
            Array.Copy(arr, 0, arr, 1, idx);
            arr[0] = tmp;

            return arr.ToList();
        }

    }
}
