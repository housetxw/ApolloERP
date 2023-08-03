using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request.OrderQuery
{
    public class GetShopGrouponProductRequest
    {

        public long ShopId { get; set; }

        public sbyte? Status { get; set; }
    }
}
