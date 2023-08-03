using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Pay
{
    public class CreateWxPrePayOrderForMiniAppRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单号不能为空")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 微信用户标识
        /// </summary>
        [Required(ErrorMessage = "微信用户标识不能为空")]
        public string OpenId { get; set; }
    }
}
