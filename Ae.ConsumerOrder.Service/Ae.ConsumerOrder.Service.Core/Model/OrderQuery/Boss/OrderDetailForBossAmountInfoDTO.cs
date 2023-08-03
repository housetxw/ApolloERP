using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    public class OrderDetailForBossAmountInfoDTO
    {
        /// <summary>
        /// 商品总价（单品或套餐，不含单服务）
        /// </summary>
        public decimal TotalProductAmount { get; set; }
        /// <summary>
        /// 服务费（单服务）
        /// </summary>
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// 总优惠金额
        /// </summary>
        public decimal TotalCouponAmount { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }
    }
}
