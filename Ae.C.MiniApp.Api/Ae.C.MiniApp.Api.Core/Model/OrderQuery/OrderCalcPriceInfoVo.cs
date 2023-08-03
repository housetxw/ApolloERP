using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    /// <summary>
    /// 订单计算价格信息
    /// </summary>
    public class OrderCalcPriceInfoVO
    {
        /// <summary>
        /// 默认推荐使用的用户最优优惠券ID（0无）
        /// 如果客户选择了非推荐优惠券，以用户选择为准
        /// </summary>
        public long UserCouponId { get; set; }
        /// <summary>
        /// 默认推荐使用的用户最优优惠券显示名称
        /// </summary>
        public string UserCouponDisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 当前适用的所有用户优惠券ID集合
        /// </summary>
        public List<long> AllApplicableCoupons { get; set; } = new List<long>();
        /// <summary>
        /// 商品总价（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
        /// </summary>
        public decimal TotalProductAmount { get; set; }
        /// <summary>
        /// 服务费（指带出的安装服务，不含套餐内安装服务）
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
        /// <summary>
        /// 包邮规则描述
        /// </summary>
        public string FreeShippingRuleDesc { get; set; } = string.Empty;

        /// <summary>
        /// 上门服务安装费
        /// </summary>
        public decimal TotalDoorToDoorFee { get; set; }
    }
}
