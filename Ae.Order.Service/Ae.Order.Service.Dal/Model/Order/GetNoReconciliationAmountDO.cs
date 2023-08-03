using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Dal.Model.Order
{
    /// <summary>
    /// 未对账的金额
    /// </summary>
    public class GetNoReconciliationAmountDO
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        /// 结算的金额
        /// </summary>
        public decimal SettlementFee { get; set; }

        /// <summary>
        /// 服务金额
        /// </summary>
        public decimal ServiceFee { get; set; }
    }
}
