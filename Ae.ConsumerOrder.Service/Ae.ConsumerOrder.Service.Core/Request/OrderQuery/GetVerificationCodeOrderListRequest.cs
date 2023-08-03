using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    public class GetVerificationCodeOrderListRequest : BasePageRequest
    {
        /// <summary>
        /// 核销门店ID
        /// </summary>
        [Required(ErrorMessage = "核销门店ID不可为空")]
        public long VerifyShopId { get; set; }
        /// <summary>
        /// 核销生成的订单号
        /// </summary>
        public string VerifyOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 核销码（RGHX*****）
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 日期范围类型（0所有 1今日 2昨日 3本月）
        /// </summary>
        public sbyte DateRangeType { get; set; }
    }
}
