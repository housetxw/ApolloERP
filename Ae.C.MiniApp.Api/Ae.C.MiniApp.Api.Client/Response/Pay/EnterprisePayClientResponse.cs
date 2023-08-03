using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Pay
{
    public class EnterprisePayClientResponse
    {
        /// <summary>
        /// 成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 微信小程序支付唤起数据
        /// </summary>
        public WxMiniAppHbCallDataDto WxMiniAppHbCallData { get; set; }
    }

    /// <summary>
    /// 唤起微信红包
    /// </summary>
    public class WxMiniAppHbCallDataDto
    {
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
