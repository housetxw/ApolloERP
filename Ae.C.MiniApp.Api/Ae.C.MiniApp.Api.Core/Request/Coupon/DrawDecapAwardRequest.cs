using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Coupon
{
    /// <summary>
    /// DrawDecapAwardRequest
    /// </summary>
    public class DrawDecapAwardRequest
    {
        /// <summary>
        /// 支付渠道
        /// </summary>
        public PayChannel PayChannel { get; set; }

        /// <summary>
        /// 兑换码
        /// </summary>
        [Required(ErrorMessage = "兑换码不能为空")]
        public string Code { get; set; }

        /// <summary>
        /// Code对应的Id
        /// </summary>
        [Range(1, Int64.MaxValue, ErrorMessage = "AwardId必须大于0")]
        public long AwardId { get; set; }

        /// <summary>
        /// 参与方的唯一标识
        /// 微信传openId
        /// 支付宝传登录账号（手机号/邮箱）、支付宝会员Id
        /// </summary>
        [Required(ErrorMessage = "Identity不能为空")]
        public string Identity { get; set; }

        /// <summary>
        /// 客户真实姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }

    /// <summary>
    /// 支付渠道
    /// </summary>
    public enum PayChannel
    {
        /// <summary>
        /// 微信
        /// </summary>
        WeChat = 0,

        /// <summary>
        /// 支付宝
        /// </summary>
        AliPay = 1
    }
}
