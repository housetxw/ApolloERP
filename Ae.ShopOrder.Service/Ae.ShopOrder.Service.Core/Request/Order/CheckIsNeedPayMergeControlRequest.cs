using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class CheckIsNeedPayMergeControlRequest:BaseGetRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> OrderNo { get; set; }

    }
}
