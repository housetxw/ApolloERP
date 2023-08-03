using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    /// <summary>
    /// 物流单号请求对象
    /// </summary>
    public class GetBatchWarehouseTransferPackagesRequest
    {
        /// <summary>
        /// 订单Ids
        /// </summary>
        public List<string> OrderIds { get; set; }

        /// <summary>
        /// 移库类型
        /// </summary>
        [Required]
        public string TransferType { get; set; }
    }
}
