using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Pay
{
    public class WeChatTransferAliPayClientRequest
    {
        /// <summary>
        /// 微信用户openId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 支付宝账号
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 交易金额（单位为元）
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
