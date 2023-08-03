using Ae.FMS.Service.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Enums
{
    public enum ChannelEnum
    {

        /// <summary>
        /// 未设置
        /// </summary>
        [EnumDescription("未设置")]
        None = 0,
        /// <summary>
        /// 总部C端
        /// </summary>
        [EnumDescription("总部C端")]
        ApolloErpToC = 1,
        /// <summary>
        /// 总部门店
        /// </summary>
        [EnumDescription("总部门店")]
        ApolloErpToShop = 2,
        /// <summary>
        /// 电销下单
        /// </summary>
        [EnumDescription("电销下单")]
        DianSale = 3,
        /// <summary>
        /// 第三方订单
        /// </summary>
        [EnumDescription("第三方订单")]
        MeiTuan = 4,
        /// <summary>
        /// 大客户
        /// </summary>
        [EnumDescription("大客户")]
        Dakehu = 5,
    }
}
