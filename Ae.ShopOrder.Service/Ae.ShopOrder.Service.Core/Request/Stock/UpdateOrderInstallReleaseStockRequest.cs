using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Stock
{
    /// <summary>
    /// 更新订单释放库存
    /// </summary>
    public class UpdateOrderInstallReleaseStockRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
