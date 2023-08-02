using Ae.B.Order.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Order.Api.Client.Request
{
    public class CreateReverseOrderBaseClientRequest
    {
        /// <summary>
        /// 操作人类型
        /// </summary>
        [Required(ErrorMessage = "必须指定操作人类型")]
        public ReverseOperatorTypeEnum OperatorType { get; set; }
        /// <summary>
        /// 用户标识，客户操作必须指定用户标识
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 操作人，客服操作必须指定操作人
        /// </summary>
        public string OperatorName { get; set; } = string.Empty;
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "必须选择订单")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 原因ID
        /// </summary>
        [Required(ErrorMessage = "必须选择原因")]
        public long ReasonId { get; set; }
        /// <summary>
        /// 选填具体说明
        /// </summary>
        public string Instruction { get; set; } = string.Empty;
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }
    }
}
