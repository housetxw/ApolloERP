using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Client.Response
{
    public class MergePayNoDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 支付单主键
        /// </summary>
        public long PayId { get; set; }

        public string PayNo { get; set; }
        public int Status { get; set; }
        public DateTime PayTime { get; set; }

        public string MergeNo { get; set; }

        public int InnerNoType { get; set; }
        /// <summary>
        /// 被合并支付的内部单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;

        public string TradeNo { get; set; }

        public int Channel { get; set; }

        public int Method { get; set; }

        public string BuyerAccountfrom { get; set; }

        public decimal Amount { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
