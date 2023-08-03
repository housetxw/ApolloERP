using Rg.Pay.Service.Core.Enums.Pay;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Pay
{
    public class ConfirmPayClientRequest
    {
        /// <summary>
        /// 内部单号类型
        /// </summary>
        public PayInnerNoTypeEnum InnerNoType { get; set; }
        /// <summary>
        /// 内部单号（根据内部单号类型决定传值）
        /// </summary>
        [Required(ErrorMessage = "内部单号不可为空")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 支付方式（0未设置 1在线支付 2到店付款）
        /// </summary>
        public PayMethodEnum Method { get; set; }
        /// <summary>
        /// 实际支付渠道（0未设置 1微信支付 2支付宝 3银联支付）
        /// </summary>
        public PayChannelEnum Channel { get; set; }
        /// <summary>
        /// 支付终端类型（0未设置 1养车小程序 2管家App）
        /// </summary>
        public PayTerminalTypeEnum TerminalType { get; set; }
        /// <summary>
        /// 支付场景类型（0未设置 1主扫收款码支付 2H5支付 3App支付 4小程序和公众号支付 5被扫付款码支付）
        /// </summary>
        public PaySceneTypeEnum SceneType { get; set; }
        /// <summary>
        /// 买家付款账号（openid或支付宝账号或Code）
        /// </summary>
        [Required(ErrorMessage = "买家付款账号或授权码不可为空")]
        public string BuyerAccount { get; set; } = string.Empty;
        /// <summary>
        /// 交易金额（单位为元）
        /// </summary>
        [Required(ErrorMessage = "交易金额不可为空")]
        //[Range(0.01d, double.MaxValue, ErrorMessage = "交易金额必须大于0")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [Required(ErrorMessage = "操作人不可为空")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 小程序ID（微信小程序支付时必传）
        /// </summary>
        [Required(ErrorMessage = "小程序ID不可为空")]
        public string AppId { get; set; } = string.Empty;
    }
}
