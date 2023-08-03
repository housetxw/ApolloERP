using Ae.ShopOrder.Service.Core.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    public class GetOrderDetailForBossResponse
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderDetailForBossOrderInfoDto OrderInfo { get; set; }
        /// <summary>
        /// 财务信息
        /// </summary>
        public OrderDetailForBossFinanceInfoDto FinanceInfo { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public OrderDetailForBossUserInfoDto UserInfo { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public OrderDetailForBossProductInfoDto ProductInfo { get; set; }
        /// <summary>
        /// 金额信息
        /// </summary>
        public OrderDetailForBossAmountInfoDto AmountInfo { get; set; }

        /// <summary>
        /// 预约信息
        /// </summary>
        public OrderDetailForBossReserverInfoDto ReserverInfo { get; set; }

        /// <summary>
        /// 技师信息
        /// </summary>
        public OrderDispatchDTO OrderDispatchInfo { get; set; }

        /// <summary>
        /// 优惠卷信息
        /// </summary>
        public OrderCouponInformationDto OrderCouponInfo { get; set; }

        public OrderInsuranceCompanyDTO OrderInsuranceCompanyInfo { get; set; }


    }

    
}
