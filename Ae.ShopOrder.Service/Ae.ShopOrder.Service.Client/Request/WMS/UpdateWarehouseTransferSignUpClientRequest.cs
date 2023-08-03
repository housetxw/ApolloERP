using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.WMS
{
    /// <summary>
    /// 更新WMS签收请求
    /// </summary>
    public class UpdateWarehouseTransferSignUpClientRequest
    {
        /// <summary>
        /// RealtionID
        /// </summary>transferId
        public string TransferId { get; set; }

        /// <summary>
        /// 移库类型
        /// </summary>
        public string TransferType { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 操作状态
        /// </summary>
        public string Status { get; set; }
    }
}
