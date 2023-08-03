using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Response.Order
{
    public class GetOrderCompleteStaticReportResponse
    {
        ///// <summary>
        ///// 已付款的订单数量
        ///// </summary>
        //public int HavePayOrderNum { get; set; }

        ///// <summary>
        ///// 未付款订单数量
        ///// </summary>
        //public int NotPayOrderNum { get; set; }

        ///// <summary>
        ///// 已付款订单金额
        ///// </summary>
        //public decimal HavePayAmount { get; set; }

        //public decimal NotPayAmount { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }

    }
}
