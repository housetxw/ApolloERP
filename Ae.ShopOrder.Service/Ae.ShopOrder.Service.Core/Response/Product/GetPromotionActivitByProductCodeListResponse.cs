using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Response.Product
{
    public class GetPromotionActivitByProductCodeListResponse
    {
        public string ProductCode { get; set; }

        public string ActicityId { get; set; }

        public decimal PromotionPrice { get; set; }

        public string Label { get; set; }

        public int AvailQuantity { get; set; }
    }
}
