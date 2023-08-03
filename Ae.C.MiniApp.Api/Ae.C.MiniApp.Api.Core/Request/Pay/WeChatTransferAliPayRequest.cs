using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Pay
{
    /// <summary>
    /// WeChatTransferAliPayRequest
    /// </summary>
    public class WeChatTransferAliPayRequest
    {
        /// <summary>
        /// 微信用户openId
        /// </summary>
        [Required(ErrorMessage = "OpenId不能为空")]
        public string OpenId { get; set; }

        /// <summary>
        /// 支付宝账号
        /// </summary>
        [Required(ErrorMessage = "Identity不能为空")]
        public string Identity { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required(ErrorMessage = "UserName不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 交易金额（单位为元）
        /// </summary>
        [Range(0.01d, 1000, ErrorMessage = "交易金额超出范围[0.01,1000]")]
        public decimal Amount { get; set; }
    }
}
