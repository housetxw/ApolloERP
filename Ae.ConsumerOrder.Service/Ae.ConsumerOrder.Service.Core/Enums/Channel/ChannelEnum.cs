using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Enums
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
        ApolloErpToShop = 2
    }
}
