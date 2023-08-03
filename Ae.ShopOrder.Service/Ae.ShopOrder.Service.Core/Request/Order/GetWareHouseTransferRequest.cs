using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    /// <summary>
    /// 物流信息请求对象
    /// </summary>
    public class GetWareHouseTransferRequest
    {
        /// <summary>
        /// 门店号
        /// </summary>
        [Required]
        public int TargetWarehouse { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        [Required]
        public long TransferId { get; set; }

        /// <summary>
        /// 出库类型
        /// </summary>
        [Required]
        public string TransferType { get; set; }
    }
}
