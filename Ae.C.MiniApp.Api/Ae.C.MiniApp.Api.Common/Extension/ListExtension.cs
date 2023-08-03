using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.C.MiniApp.Api.Common.Extension
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            if (list == null || list.Count() == 0)
            {
                return true;
            }
            return false;
        }
    }
}
