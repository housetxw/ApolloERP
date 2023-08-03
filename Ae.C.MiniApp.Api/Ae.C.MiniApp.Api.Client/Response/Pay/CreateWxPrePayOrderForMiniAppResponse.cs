using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Pay
{
    public class CreateWxPrePayOrderForMiniAppResponse
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        public string PayNo { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 统一下单接口返回的 prepay_id 参数值，提交格式如：prepay_id=*
        /// </summary>
        public string Package { get; set; }
        /// <summary>
        /// 签名类型，默认为MD5
        /// </summary>
        public string SignType { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string PaySign { get; set; }
    }
}
