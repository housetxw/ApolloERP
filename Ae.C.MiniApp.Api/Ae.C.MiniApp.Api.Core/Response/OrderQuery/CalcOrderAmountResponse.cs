using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    /// <summary>
    /// 试算订单金额返回参数
    /// </summary>
    public class CalcOrderAmountResponse : OrderCalcPriceInfoVO
    {
        /// <summary>
        ///钻石积分优惠金额
        /// </summary>
        public decimal DiamondsDiscountAmount { get; set; }
    }
}
