using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Ae.FMS.Service.Common.CustomAttribute;

namespace Ae.FMS.Service.Core.Enums
{
    public enum PayChannelEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [EnumDescription("未设置")]
        None = 0,
        /// <summary>
        /// 微信支付
        /// </summary>
        [EnumDescription("微信支付")]
        WeiXin = 1,
        /// <summary>
        /// 支付宝
        /// </summary>
        [EnumDescription("支付宝")]
        ZhiFuBao = 2,
        /// <summary>
        ///美团
        /// </summary>
        [EnumDescription("美团")]
        MeiTuan = 3,
        /// <summary>
        ///线下支付
        /// </summary>
        [EnumDescription("线下支付")]
        Cash = 4,
        /// <summary>
        ///代付款
        /// </summary>
        [EnumDescription("代付款")]
        DaiZhiFu = 9
    }
}
