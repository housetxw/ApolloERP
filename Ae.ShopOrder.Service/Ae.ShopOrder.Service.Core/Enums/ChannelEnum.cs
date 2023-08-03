using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// 订单渠道
    /// </summary>
    public enum ChannelEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// C端
        /// </summary>
        [Description("C端")]
        ApolloErpToC = 1,
        /// <summary>
        /// 门店
        /// </summary>
        [Description("门店")]
        ApolloErpToShop = 2,
        /// <summary>
        /// 电销下单
        /// </summary>
        [Description("电销下单")]
        DianSale = 3,
        /// <summary>
        /// 第三方订单
        /// </summary>
        [Description("第三方订单")]
        MeiTuan = 4,
        /// <summary>
        /// 大客户
        /// </summary>
        [Description("大客户")]
        Dakehu = 5,
    }
}
