using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Common.Util
{
    public static class ShopTypeUtil
    {
        public static string ConvertShopType(int shopType)
        {
            switch (shopType)
            {
                case 1:
                    return "合作店";
                case 2:
                    return "直营店";
                case 4:
                    return "上门养护";
                case 8:
                    return "认证店";
            }

            return string.Empty;
        }
    }
}
