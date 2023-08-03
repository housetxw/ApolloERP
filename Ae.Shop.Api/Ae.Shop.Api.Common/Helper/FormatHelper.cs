using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Common.Helper
{
    public class FormatHelper
    {
        public static string FormatTel(string tel)
        {
            if (string.IsNullOrEmpty(tel))
            {
                return string.Empty;
            }

            int length = tel.Length;
            if (length < 4)
            {
                return tel.PadRight(11, '*');
            }
            else
            {
                return tel.Substring(0, 3) + "****" + tel.Substring(length - 4, 4);
            }
        }
    }
}
