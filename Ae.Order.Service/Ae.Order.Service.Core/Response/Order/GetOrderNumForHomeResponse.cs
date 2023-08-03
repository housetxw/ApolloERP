using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Response.Order
{
    /// <summary>
    /// 得到订单的数量（待签收、待安装、未对账、异常中、已取消） 返回对象
    /// </summary>
    public class GetOrderNumForHomeResponse
    {
        /// <summary>
        /// 待签收数量
        /// </summary>
        public int WaitingSignNum { get; set; }

        /// <summary>
        /// 待安装数量
        /// </summary>
        public int WaitingInstallNum { get; set; }

        /// <summary>
        /// 未对账数量
        /// </summary>
        public int WaitingReconciliationNum { get; set; }

        /// <summary>
        /// 对账异常的数量
        /// </summary>
        public int ExceptionReconciliationNum { get; set; }

        /// <summary>
        /// 已取消数量
        /// </summary>
        public  int CanceledNum { get; set; }
    }
}
