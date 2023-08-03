using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Response.Settlement
{
    /// <summary>
    /// 得到财务账单列表返回对象
    /// </summary>
    public class GetFianceReconciliationStatusListResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 展示的状态
        /// </summary>
        public string ShowStatus { get; set; }

        /// <summary>
        /// 展示安装时间
        /// </summary>
        public DateTime InstallTime { get; set; }

        /// <summary>
        /// 展示总售价
        /// </summary>
        public decimal SaleTotalAmount { get; set; }

        /// <summary>
        /// 展示服务费
        /// </summary>
        public decimal ServiceFee { get; set; }

        /// <summary>
        ///应结算的金额
        /// </summary>
        public decimal SettlementFee { get; set; }

    }
}
