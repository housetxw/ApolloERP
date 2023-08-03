using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.OrderQuery
{
    public class GetPackageCardMainInfoResponse
    {
        /// <summary>
        /// 订单号（RGC或RGS前缀）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;

        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 套餐卡名称
        /// </summary>
        public string Name { get; set; }
    }
}
