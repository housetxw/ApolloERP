using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    public enum PayChannelEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 微信支付
        /// </summary>
        [Description("微信支付")]
        WeiXin = 1,
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        ZhiFuBao = 2,
        /// <summary>
        ///美团
        /// </summary>
        [Description("美团")]
        MeiTuan = 3,
        /// <summary>
        ///线下支付
        /// </summary>
        [Description("线下支付")]
        Cash = 4,
        /// <summary>
        ///代付款
        /// </summary>
        [Description("代付款")]
        DaiZhiFu = 9
    }
}
