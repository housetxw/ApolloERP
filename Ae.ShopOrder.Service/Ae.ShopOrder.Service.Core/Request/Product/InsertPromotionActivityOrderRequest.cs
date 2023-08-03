using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Product
{
    public class InsertPromotionActivityOrderRequest
    {
        public string OrderNo { get; set; }

        public string UserId { get; set; }

        public string SubmitBy { get; set; }

        public List<PromotionProductVo> Products { get; set; }
    }
    public class PromotionProductVo
    {
        public string ProductCode { get; set; }

        public  int Num { get; set; }

        public string ActivityId { get; set; }
    }
}
