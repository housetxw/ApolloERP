using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
{
    public class GetOrderDetailForBossResponse
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderDetailForBossOrderInfoDTO OrderInfo { get; set; }
        /// <summary>
        /// 财务信息
        /// </summary>
        public OrderDetailForBossFinanceInfoDTO FinanceInfo { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public OrderDetailForBossUserInfoDTO UserInfo { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public OrderDetailForBossProductInfoDTO ProductInfo { get; set; }
        /// <summary>
        /// 金额信息
        /// </summary>
        public OrderDetailForBossAmountInfoDTO AmountInfo { get; set; }
    }
}
