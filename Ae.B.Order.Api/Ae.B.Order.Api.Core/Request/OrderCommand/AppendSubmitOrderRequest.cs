using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Order.Api.Core.Request
{
    public class AppendSubmitOrderRequest
    {
        /// <summary>
        /// 原订单号
        /// </summary>
        [Required(ErrorMessage = "原订单号不能为空")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 被追加下单的商品编号
        /// </summary>
        [Required(ErrorMessage = "被追加下单的商品编号不能为空")]
        public string AppendForProductId { get; set; } = string.Empty;
        /// <summary>
        /// 追加数量
        /// </summary>
        [Required(ErrorMessage = "追加数量不可为空")]
        public int Number { get; set; } = 1;
    }
}
