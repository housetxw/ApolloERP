using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.ShopReport
{
    public class ShopSaleMonthVO
    {
        /// <summary>
        /// 类型：0合计，1-6订单类型，7套餐卡
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 客户数
        /// </summary>
        public int CustomerNum { get; set; }
        /// <summary>
        /// 订单数
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal OrderSumMoney { get; set; }
        /// <summary>
        /// 客户均金额
        /// </summary>
        public decimal CustomerAvgMoney { get; set; }
        /// <summary>
        /// 订单均金额
        /// </summary>
        public decimal OrderAvgMoney { get; set; }
    }
}
