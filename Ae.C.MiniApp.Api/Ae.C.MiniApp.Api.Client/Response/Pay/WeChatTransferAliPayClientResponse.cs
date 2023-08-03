using Ae.C.MiniApp.Api.Core.Response;
using Rg.Pay.Service.Core.Enums.Pay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Pay
{
    public class WeChatTransferAliPayClientResponse
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        public string PayNo { get; set; } = string.Empty;

        /// <summary>
        /// 支付状态（0新建 1已调用等待回调中 2成功 3失败 4取消 5关闭）
        /// </summary>
        public PayStatusEnum Status { get; set; }

        /// <summary>
        /// 微信小程序支付唤起数据
        /// </summary>
        public WxMiniAppPayCallData WxMiniAppPayCallData { get; set; }
    }
}
