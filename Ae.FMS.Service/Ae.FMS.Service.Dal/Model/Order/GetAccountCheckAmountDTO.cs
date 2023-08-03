﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Dal.Model.Order
{
  public  class GetAccountCheckAmountDTO
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
