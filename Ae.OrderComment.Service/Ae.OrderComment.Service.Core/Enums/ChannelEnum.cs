using Ae.OrderComment.Service.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Enums
{
    /// <summary>
    /// 订单渠道
    /// </summary>
    public enum ChannelEnum
    {
        [EnumDescription("未设置")]
        None = 0,
        /// <summary>
        /// 平台C端
        /// </summary>
        [EnumDescription("平台C端")]
        ApolloErpToC = 1,
        /// <summary>
        /// 平台门店
        /// </summary>
        [EnumDescription("平台门店")]
        ApolloErpToShop = 2
    }
}
