using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
{
    /// <summary>
    /// 试算订单金额返回参数
    /// </summary>
    public class TrialCalcOrderAmountResponse : OrderCalcPriceInfoDTO
    {
        /// <summary>
        ///钻石积分优惠金额
        /// </summary>
        public decimal DiamondsDiscountAmount { get; set; }
    }
}
