using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Common.Util
{
    public static class StringUtil
    {
        public static bool HasLength(this string target)
        {
            return (target != null && target.Length > 0);
        }
        public static bool IsNullOrEmpty(this string target)
        {
            return !HasText(target);
        }
        public static bool HasText(this string target)
        {
            if (target == null)
            {
                return false;
            }
            else
            {
                return HasLength(target.Trim());
            }
        }
        public static string GetTextOrNull(this string value)
        {
            if (!HasText(value))
            {
                return null;
            }
            return value;
        }

        public static int GetIntValue(this int? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }

            return 0;
        }
    }
}
