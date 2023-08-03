using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request.OrderQuery
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
