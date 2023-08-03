using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class BatchUpdateInstallStatusRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> OrderNo { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
