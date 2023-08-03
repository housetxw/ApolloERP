using Rg.Pay.Service.Core.Enums.Pay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Pay
{
    public class EnterprisePayClientRequest
    {
        /// <summary>
        /// 内部单号类型
        /// </summary>
        public PayInnerNoTypeEnum InnerNoType { get; set; }

        /// <summary>
        /// 内部单号（根据内部单号类型决定传值）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;

        /// <summary>
        /// 实际支付渠道（0未设置 1微信支付 2支付宝 3银联支付）
        /// </summary>
        public PayChannelEnum Channel { get; set; }

        /// <summary>
        /// 交易金额（单位为元）
        /// </summary>
        //[Range(0.01d, double.MaxValue, ErrorMessage = "交易金额必须大于0")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 参与方的唯一标识
        /// 微信传openId
        /// 支付宝传登录账号（手机号/邮箱）、支付宝会员Id
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 客户真实姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否红包支付
        /// </summary>
        public bool IsHb { get; set; }
    }
}
