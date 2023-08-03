using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.WMS
{
    /// <summary>
    /// 入库请求
    /// </summary>
    public class UpdateWarehouseTransferProductReceiveNumClientRequest
    {
        /// <summary>
        /// RealtionId
        /// </summary>
        public string TransferId { get; set; }

        /// <summary>
        /// 移库类型
        /// </summary>
        public string TransferType { get; set; }

        public  string UpdateBy { get; set; }

        public List<TransferProduct> TransferList { get; set; }
    }

    public class TransferProduct
    {
        /// <summary>
        /// 产品的Id
        /// </summary>
        public long TransferProductId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int ReceiveNum { get; set; }
    }


}
