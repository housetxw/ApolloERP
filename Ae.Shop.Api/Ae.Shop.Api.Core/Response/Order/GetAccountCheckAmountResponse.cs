using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response.Order
{
    /// <summary>
    /// 对账金额返回对象
    /// </summary>
    public class GetAccountCheckAmountResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// OrderId
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 实际金额
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceFee { get; set; }

        /// <summary>
        /// 总成本
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal OtherFee { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal CommissionFee { get; set; }

        /// <summary>
        /// 结算的金额
        /// </summary>
        public decimal SettlementFee { get; set; }
    }
}
