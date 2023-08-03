using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.WMS
{
    /// <summary>
    /// 更新包裹的请求
    /// </summary>
    public class UpdateWarehouseTransferPackageStatusClientRequest
    {
        /// <summary>
        /// Relation_ID
        /// </summary>
        public string TransferId { get; set; }

        /// <summary>
        /// 移库类型
        /// </summary>
        public  string TransferType { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 包裹号
        /// </summary>
        public  List<string> DeliveryCodes { get; set; }
    }
}
