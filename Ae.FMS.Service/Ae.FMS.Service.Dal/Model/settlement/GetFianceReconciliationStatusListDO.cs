using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Dal.Model.settlement
{
    public class GetFianceReconciliationStatusListDO
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
        public string InstallTime { get; set; }

        /// <summary>
        /// 展示总售价
        /// </summary>
        public string SaleTotalAmount { get; set; }

        /// <summary>
        /// 展示服务费
        /// </summary>
        public string ServiceFee { get; set; }

        /// <summary>
        ///应结算的金额
        /// </summary>
        public string SettlementFee { get; set; }
    }
}
