using Rg.Pay.Service.Core.Enums.Pay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class ConfirmPayResponse
    {
        /// <summary>
        /// 是否唤起支付
        /// </summary>
        public bool ArousePayment { get; set; }

        /// <summary>
        /// 支付单号
        /// </summary>
        public string PayNo { get; set; } = string.Empty;
        /// <summary>
        /// 内部单号类型
        /// </summary>
        public PayInnerNoTypeEnum InnerNoType { get; set; }
        /// <summary>
        /// 内部单号（根据内部单号类型决定传值）
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 支付场景类型（0未设置 1微信小程序支付 2微信扫码支付 3 支付宝扫码支付）
        /// </summary>
        public PaySceneTypeEnum SceneType { get; set; }
        /// <summary>
        /// 支付状态（0新建 1已调用等待回调中 2成功 3失败 4取消 5关闭）
        /// </summary>
        public PayStatusEnum Status { get; set; }
        /// <summary>
        /// 微信小程序支付唤起数据
        /// </summary>
        public WxMiniAppPayCallData WxMiniAppPayCallData { get; set; }
    }

    public class WxMiniAppPayCallData
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
