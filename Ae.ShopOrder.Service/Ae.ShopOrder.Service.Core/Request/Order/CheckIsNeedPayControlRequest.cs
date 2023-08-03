using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class CheckIsNeedPayControlRequest:BaseGetRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> OrderNo { get; set; }

        /// <summary>
        /// shopId 
        /// </summary>
        public int ShopId { get; set; }
    }
}
