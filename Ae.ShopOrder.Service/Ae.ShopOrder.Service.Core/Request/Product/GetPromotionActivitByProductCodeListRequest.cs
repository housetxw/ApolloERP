using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Product
{
    public class GetPromotionActivitByProductCodeListRequest
    {
        public List<string> ProductCodeList { get; set; }

        public string PromotionChannel { get; set; }
    }
}
