using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Core.Model.OrderDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Client.Response
{
    public class GetOrderDetailForBossClientResponse
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

        /// <summary>
        /// 保险公司信息
        /// </summary>
        public OrderInsuranceCompanyDTO OrderInsuranceCompanyInfo { get; set; }
    }
}
