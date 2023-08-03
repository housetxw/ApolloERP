using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.WMS
{
    /// <summary>
    /// 物流请求对象
    /// </summary>
    public class GetWareHouseTransferPackageClientRequest
    {
        /// <summary>
        /// 物流单号
        /// </summary>
        public string DeliveryCode { get; set; }

        /// <summary>
        /// 物流公司
        /// </summary>
        public string DeliveryCompany { get; set; }
    }
}
