using Ae.Order.Service.Core.Enums;
using Ae.Order.Service.Core.Model.Order;

namespace Ae.Order.Service.Core.Request.Order
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
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }
    }
}
