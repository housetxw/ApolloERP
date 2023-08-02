using Ae.B.Order.Api.Core.Model;
using Ae.B.Order.Api.Core.Model.OrderDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Response
{
    public class GetOrderDetailResponse
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderDetailForBossOrderInfoVO OrderInfo { get; set; }
        /// <summary>
        /// 财务信息
        /// </summary>
        public OrderDetailForBossFinanceInfoVO FinanceInfo { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public OrderDetailForBossUserInfoVO UserInfo { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public OrderDetailForBossProductInfoVO ProductInfo { get; set; }
        /// <summary>
        /// 金额信息
        /// </summary>
        public OrderDetailForBossAmountInfoVO AmountInfo { get; set; }

        /// <summary>
        /// 预约信息
        /// </summary>
        public OrderDetailForBossReserverInfoVo ReserverInfo { get; set; }


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
