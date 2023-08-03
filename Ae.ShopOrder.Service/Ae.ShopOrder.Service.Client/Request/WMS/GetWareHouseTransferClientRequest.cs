using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.WMS
{
    public class GetWareHouseTransferClientRequest
    {
        /// <summary>
        /// 门店号
        /// </summary>
        public int TargetWarehouse { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>

        public  string TransferId { get; set; }

        /// <summary>
        /// 出库类型
        /// </summary>
        public string TransferType { get; set; }
    }
}
