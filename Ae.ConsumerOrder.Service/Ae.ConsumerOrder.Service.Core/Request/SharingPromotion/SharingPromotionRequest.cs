using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request.SharingPromotion
{
    public class SharingPromotionRequest : BaseGetRequest
    {


        public string UserId { get; set; }

        public string OrderNo { get; set; }

        public decimal ActualAmount { get; set; }
    }
}
