using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    /// <summary>
    /// 再次购买请求参数
    /// </summary>
    public class BuyAgainRequest
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
