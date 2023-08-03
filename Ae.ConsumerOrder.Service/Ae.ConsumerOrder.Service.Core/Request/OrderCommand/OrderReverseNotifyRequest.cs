using Ae.ConsumerOrder.Service.Core.Enums;
using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    public class OrderReverseNotifyRequest : BaseOrderOperationConditionDTO
    {
        /// <summary>
        /// 逆向申请单号
        /// </summary>
        public string ReverseNo { get; set; }
        /// <summary>
        /// 逆向申请类型
        /// </summary>
        public ReverseApplyTypeEnum ApplyType { get; set; }
        /// <summary>
        /// 逆向申请单状态
        /// </summary>
        public ReverseStatusEnum ReverseStatus { get; set; }
        /// <summary>
        /// 退款状态（0未退款 1已退款）
        /// </summary>
        public sbyte RefundStatus { get; set; }
    }
}
