using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class PayRequest : BaseGetRequest
    {
        /// <summary>
        /// 到店记录
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// shopId 
        /// </summary>
        public int ShopId { get; set; }
    }
}
