using Ae.B.Order.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Client.Request
{
    public class CheckOrderClientRequest : BaseOrderOperationConditionDTO
    {
        /// <summary>
        /// 审核类型（0未设置 1在线支付自动审核 2到店支付人工审核）
        /// </summary>
        public sbyte CheckType { get; set; }
        /// <summary>
        /// 是否审核通过
        /// </summary>
        public bool IsCheckPass { get; set; }
    }
}
