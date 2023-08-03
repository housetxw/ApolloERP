using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Order
{
    public class BuyAgainClientRequest
    {
        /// <summary>
        /// 原订单号
        /// </summary>
        [Required(ErrorMessage = "原订单号不能为空")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string UserId { get; set; }
    }
}
