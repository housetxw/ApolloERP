using System;
using System.Collections.Generic;
using System.Text;
using Ae.ShopOrder.Service.Core.Enums;

namespace Ae.ShopOrder.Service.Core.Request.OrderForC
{

    /// <summary>
    /// 更新订单签收状态请求
    /// </summary>
    public class UpdateOrderStatusRequest
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public List<string> OrderNos { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新订单状态类型
        /// </summary>
        public UpdateOrderStatusTypeEnum UpdateStatusType { get; set; }
    }
}
