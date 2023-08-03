using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Reserve
{
    /// <summary>
    /// 取消预约
    /// </summary>
    public class CancelReserveRequest
    {
        /// <summary>
        /// 预约Id
        /// </summary>
        [Required(ErrorMessage = "预约Id不能为空")]
        [Range(1, Int32.MaxValue, ErrorMessage = "预约Id必须大于0")]
        public long ReserveId { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        [Required(ErrorMessage = "取消原因不能为空")]
        public string CancelReason { get; set; }
    }
}
