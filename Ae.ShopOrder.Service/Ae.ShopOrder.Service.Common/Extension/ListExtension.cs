using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Common.Extension
{
    public static class ListExtension
    {
        /// <summary>
        /// 判断List<T> a里面的项是否每一个都和List<T> b一致(顺序可以不一样)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equal_EveryItem<T>(this List<T> a, List<T> b)
        {
            if (a == null || b == null || a.Count <= 0 || b.Count <= 0 || a.Count != b.Count) return false;
            bool result = true;
            foreach (var a_item in a)
            {
                if (b.Exists(b_item => b_item.Equals(a_item)))
                {
                    b.Remove(a_item);
                }
                else
                {//不存在
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
