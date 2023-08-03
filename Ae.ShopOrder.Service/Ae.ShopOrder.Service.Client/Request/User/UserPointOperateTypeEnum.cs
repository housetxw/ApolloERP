using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.User
{
    public enum UserPointOperateTypeEnum
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        [Description("注册用户")]
        Register = 0,

        /// <summary>
        /// 支付订单
        /// </summary>
        [Description("支付订单")]
        PayOrder = 1,

        /// <summary>
        /// 订单点评
        /// </summary>
        [Description("订单点评")]
        OrderComment = 2,

        /// <summary>
        /// 精华点评
        /// </summary>
        [Description("精华点评")]
        EssenceComment = 3,

        /// <summary>
        /// 兑换优惠券
        /// </summary>
        [Description("兑换优惠券")]
        RedeemCoupons = 4,
        /// <summary>
        /// 推荐新用户
        /// </summary>
        [Description("推荐新用户")]
        ReferrerNewUser = 5,
        /// <summary>
        /// 推荐订单
        /// </summary>
        [Description("推荐订单")]
        ReferrerOrder = 6,
        /// <summary>
        /// 积分收回
        /// </summary>
        [Description("积分收回")]
        Withdraw = 7,
        /// <summary>
        /// 积分提现
        /// </summary>
        [Description("积分提现")]
        Cashout = 8
    }
}
